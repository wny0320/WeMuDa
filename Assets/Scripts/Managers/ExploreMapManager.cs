using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class ExploreMapManager : MonoBehaviour
{
    public enum RoomKind
    {
        //미지정
        non = -2,
        //탐험 시작 지점
        start = -1,
        //식수를 얻거나 낚시를 할 수 있는 동굴 호수 방
        lake,
        //동물과의 전투를 벌이는 방
        battle,
        //버려진 다른 베이스 캠프가 있는 방
        baseCamp,
        //광물지대 방
        mine,
        //함정 방
        trap,
        //버려진 아이템이 있는 방
        item,
        //고립된 다른 광부가 있는 방
        miner,
    }
    private static ExploreMapManager instance;
    public static ExploreMapManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ExploreMapManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<ExploreMapManager>();
                    obj.name = typeof(ExploreMapManager).Name;
                }
            }
            return instance;
        }
    }
    // 맵 생성 관련 데이터들
    private Vector2[,] loca;
    private bool[,] locaFlag;

    private Vector2 startPos;
    private GameObject startPoint;

    [SerializeField]
    private GameObject roomPrefab;
    private GameObject mapCanvasObject;
    [SerializeField]
    private GameObject cursorPrefab;
    private GameObject cursor;

    private int mapSize = 0;
    [SerializeField]
    private int maxMapSize = 20;

    private int nowX;
    private int nowY;

    //roomInfo 관련 field
    public List<RoomInformation> RoomInformationList;
    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEvent;

    private GameObject targetRoom = null;
    private Image roomInfoImage;
    private TMP_Text roomName;
    private TMP_Text roomExplain;
    private GameObject roomInfoObject;
    [SerializeField]
    private Sprite exploreSprite;
    private float roomNowTime = 0f;
    [SerializeField, Tooltip("맵의 정보를 띄우는데 걸리는 시간"), Range(0, 1)]
    private float roomMaxTime = 1f;

    // 씬 전환시 필요한 데이터들
    private Coroutine loop = null;
    private bool IsDateSet = false;

    // 탐험을 나갈 수 있는 광부의 상수
    public const int CanExploreMinerCount = 5;
    // 탐험을 나간 광부들의 리스트
    public List<Miner> ExploringMinerList = new List<Miner>();  // 아직 데이터를 추가하지 않았음
    private void ExploreSetting()
    {
        // Scene이 로드 된 때가 아닌경우
        // 즉 Update가 한번 돌고 다음턴에 LateUpdate로 IsSceneLoaded가 false가 된 경우
        if (GameSceneManager.Instance.IsSceneLoaded == false) return;
        if(loop == null) loop = StartCoroutine(sceneNameLoop());
    }
    private void findData()
    {
        canvas = GameObject.Find("RoomCanvas").GetComponent<Canvas>();
        mapCanvasObject = GameObject.Find("RoomCanvas");
        roomInfoObject = GameObject.Find("RoomInfoCanvas/RoomInfoBackground");
        roomInfoImage = roomInfoObject.transform.Find("RoomImage").GetComponent<Image>();
        roomName = roomInfoObject.transform.Find("RoomNameText").GetComponent<TMP_Text>();
        roomExplain = roomInfoObject.transform.Find("RoomExplainText").GetComponent<TMP_Text>();
    }
    // Scene이 바뀌었지만 아직 로드가 안된경우 비동기식으로 바뀐 Scene의 이름을 찾고 그 Scene이 해당하는 씬일 경우 세팅
    private IEnumerator sceneNameLoop()
    {
        while (GameSceneManager.Instance.IsSceneNameChanged == false)
        {
            yield return new WaitForEndOfFrame();
        }
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene)
        {
            loop = null;
            IsDateSet = false;
            yield break;
        }

        findData(); // 매니저가 필요한 데이터를 찾는 함수
        roomInfoDataReset(); // RoomInfo를 띄우기 위한 데이터를 찾는 함수
        GetSize(); // 맵크기를 받는 함수
        SetStartPoint(); // 시작 지점을 지정하는 함수
        SceneLoadedFunc(); // Scene이 로드되었을 때 맵을 만드는 함수
        if (GameSceneManager.Instance.IsExploreSceneLoaded == true) RoomDataImport();
        SetCursor();
        BackgroundManager.Instance.SetBackground(exploreSprite);
        roomInfoActive(false); // roomInfo 오브젝트를 비활성화 하는 함수
        StopCoroutine(sceneNameLoop()); // 이 Coroutine 정지
        loop = null; // Coroutine을 비움
        IsDateSet = true; // 데이터 셋팅이 다 되었다고 표시
        yield break;
    }
    private void GetSize()
    {
        Vector2 _leftBottom = CameraManager.Instance.cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 _rightUp = CameraManager.Instance.cam.ViewportToWorldPoint(new Vector2(1, 1));
        int _x = (int)Mathf.Round(_rightUp.x - _leftBottom.x);
        int _y = (int)Mathf.Round(_rightUp.y - _leftBottom.y);
        loca = new Vector2[_x, _y];
        locaFlag = new bool[_x, _y];
        for (int i = 0; i < loca.GetLength(0); i++)
        {
            for (int j = 0; j < loca.GetLength(1); j++)
            {
                loca[i, j] = new Vector2(_leftBottom.x + i, _leftBottom.y + j);
            }
        }
    }

    private void SetStartPoint()
    {
        startPos = CameraManager.Instance.cam.WorldToScreenPoint(loca[1, 5]);
        startPoint = Instantiate(roomPrefab);
        startPoint.name = "StartPoint";
        Room startRoom = startPoint.GetComponent<Room>();
        startRoom.StartRoomSet(loca[1, 5], startPos, new int[] { 1, 5 });
        startPoint.transform.SetParent(mapCanvasObject.transform);
        nowX = 1;
        nowY = 5;
        locaFlag[nowX, nowY] = true;
        startPoint.transform.position = startPos;
    }
    public void UpdateNowXY(int _x, int _y)
    {
        nowX = _x;
        nowY = _y;
    }
    private void SetCursor()
    {
        cursor = Instantiate(cursorPrefab);
        cursor.transform.SetParent(mapCanvasObject.transform);
        cursor.transform.position = new Vector2(startPos.x + 50f, startPos.y + 50f);
    }
    private void CursorUpdate()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene) return;
        if (IsDateSet == false) return;
        Vector2 _cursorPos = CameraManager.Instance.cam.WorldToScreenPoint(loca[nowX, nowY]);
        cursor.transform.position = new Vector2(_cursorPos.x + 50f, _cursorPos.y + 50f);
    }
    private void MapMaking(int _xPos, int _yPos)
    {
        int _x = _xPos;
        int _y = _yPos;
        //Debug.Log(mapSize);
        if (_x > loca.GetLength(0) || _y > loca.GetLength(1))
            return;
        for (int j = _y - 1; j <= _y + 1; j++)
        {
            for (int i = _x - 1; i <= _x + 1; i++)
            {
                //배열 범위 밖인지 판단
                if (IsOutOfRange(i, j))
                    continue;
                //기준 위치와 같은 위치인 경우
                if (i == _x || i == _y)
                    continue;
                //현재 생성위치에 이미 방이 있는 경우
                if (locaFlag[i, j])
                    continue;

                int _cnt = 0;
                //배열 범위에서 상하좌우에 방이 있는 경우(인접한 방이 있는경우)인지 판단
                //방이 비어있지 않다면 cnt++
                if (!IsOutOfRange(i - 1, j))
                    if (locaFlag[i - 1, j])
                        _cnt++;
                if (!IsOutOfRange(i + 1, j))
                    if (locaFlag[i + 1, j])
                        _cnt++;
                if (!IsOutOfRange(i, j - 1))
                    if (locaFlag[i, j - 1])
                        _cnt++;
                if (!IsOutOfRange(i, j + 1))
                    if (locaFlag[i, j + 1])
                        _cnt++;

                //Debug.Log(_cnt);
                //cnt가 0인 경우, 즉 인접한 방이 없는 경우 continue
                if (_cnt == 0)
                    continue;

                //방이 없다면 50% 확률로 방을 생성
                bool _dice = Random.value > .5;
                if (_dice)
                {
                    GameObject _target = Instantiate(roomPrefab);
                    _target.transform.SetParent(mapCanvasObject.transform);
                    Room _targetRoom = _target.GetComponent<Room>();
                    Vector2 _targetPos = CameraManager.Instance.cam.WorldToScreenPoint(loca[i, j]);
                    _targetRoom.RoomInstiate(loca[i, j], _targetPos, new int[2] { i, j });
                    locaFlag[i, j] = true;
                    mapSize++;
                }
                if (mapSize >= maxMapSize)
                    return;
                else
                    MapMaking(i, j);
            }
        }
    }
    private bool IsOutOfRange(int _x, int _y)
    {
        //x값 배열 범위 밖
        if (loca.GetLength(0) - 1 <= _x || _x < 1)
            return true;
        //y값 배열 범위 밖
        if (loca.GetLength(1) - 1 <= _y || _y < 1)
            return true;
        return false;
    }
    /// <summary>
    /// 현재 위치 기준으로 _roomNum에 해당하는 좌표의 방이 인접했는지를 bool로 반환
    /// </summary>
    /// <param name="_roomNum">방 좌표 loca값</param>
    /// <returns></returns>
    public bool IsNear(int[] _roomNum)
    {
        int x = _roomNum[0];
        int y = _roomNum[1];
        if ((x - 1 == nowX || x + 1 == nowX) && y == nowY)
            return true;
        if ((y - 1 == nowY || y + 1 == nowY) && x == nowX)
            return true;


        //for (int i = x - 1; i <= x + 1; i++)
        //{
        //    for (int j = y - 1; j <= y + 1; j++)
        //    {
        //        if(nowX == i && nowY == j)
        //        {
        //            return true;
        //        }
        //    }
        //}
        return false;
    }
    private void roomInfoDataReset()
    {
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEvent = new PointerEventData(null);
    }
    private void roomInfoActive(bool _tf)
    {
        roomInfoObject.SetActive(_tf);
    }
    /// <summary>
    /// 마우스를 방에 1초간 두면 방의 정보를 띄워주는 함수
    /// </summary>
    private void roomInfo()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene) return;
        if (IsDateSet == false) return;
        pointerEvent.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEvent, results);
        if (results.Count > 0)
        {
            if (targetRoom == results[0].gameObject && results[0].gameObject.TryGetComponent<Image>(out Image _image))
            {
                if (roomNowTime >= roomMaxTime && _image.sprite != null)
                {
                    //정보를 띄워줌
                    RoomInformation info = results[0].gameObject.GetComponentInParent<Room>().ReturnRoomInfo();
                    roomInfoImage.sprite = info.RoomSprite;
                    roomName.text = info.RoomName;
                    roomExplain.text = info.RoomExplain;
                    roomInfoObject.transform.position = results[0].gameObject.transform.position;
                    roomInfoActive(true);
                }
                roomNowTime += Time.deltaTime;
            }
            else
            {
                targetRoom = results[0].gameObject;
                roomInfoActive(false);
                roomNowTime = 0f;
            }
        }
        else
        {
            roomInfoActive(false);
        }
    }
    public void SetExploreCanvas(bool _tf)
    {
        mapCanvasObject.SetActive(_tf);
    }
    public void StartExplore()
    {
        ExploringMinerList = MinerInfoManager.Instance.ExploreMinerList.ToList();
        // 해당하는 광부들의 탐험을 시작함
    }
    public void BattleModeToExploreMode()
    {
        BattleManager.Instance.SetBattleCanvas(false);
        SetExploreCanvas(true);
    }
    public void SceneLoadedFunc()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene) return;
        if (GameSceneManager.Instance.IsExploreSceneLoaded == true) return; // 이전에 로드된 적이 있다면 함수 종료
        MapMaking(nowX, nowY);
    }
    public void RoomDataExport()
    {
        Room[] roomArray = mapCanvasObject.GetComponentsInChildren<Room>();
        List<RoomJsonData> roomJsonDatasList = new List<RoomJsonData>();
        int roomCount = roomArray.Length;
        for (int i = 0; i < roomCount; i++)
        {
            RoomJsonData m_roomJsonData = roomArray[i].ReturnRoomJsonData();
            //시작 방일 경우 스킵
            if (m_roomJsonData.roomType == -1) continue;
            roomJsonDatasList.Add(m_roomJsonData);
        }
        string json = JsonConvert.SerializeObject(roomJsonDatasList);
        string path = Path.Combine(Application.persistentDataPath, "RoomData.json");
        File.WriteAllText(path, json);
        //Application.dataPath; 에디터에서만 접근이 가능한 저장 위치
        //Application.streamingAssetsPath; 읽기만 가능한 저장 위치
        //Application.persistentDataPath; 읽기 쓰기가 모두 가능한 저장 위치
        //아니면 PlayerPrefs로 저장 가능
    }
    public void RoomDataImport()
    {
        List<RoomJsonData> roomJsonDatasList = new List<RoomJsonData>();
        string path = Path.Combine(Application.persistentDataPath, "RoomData.json");
        if (File.Exists(path) == false) Debug.LogError("저장된 json파일이 존재하지 않습니다!");
        string json = File.ReadAllText(path);
        roomJsonDatasList = JsonConvert.DeserializeObject<List<RoomJsonData>>(json);
        StartCoroutine(InstantiateRoomFromJson(roomJsonDatasList));
    }
    private IEnumerator InstantiateRoomFromJson(List<RoomJsonData> _roomJsonDatasList)
    {
        //시작 방은 제외했으므로 다시 생성
        //커서 만들어줘야 함
        //다른 코드에서 만들어줌
        //SetStartPoint();
        while(GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene)
        {
            yield return new WaitForEndOfFrame();
        }
        int count = _roomJsonDatasList.Count;
        for(int i = 0; i < count; i++)
        {
            Room m_room = Instantiate(roomPrefab).GetComponent<Room>();
            m_room.transform.SetParent(mapCanvasObject.transform);
            int m_roomNumX = _roomJsonDatasList[i].roomNumX;
            int m_roomNumY = _roomJsonDatasList[i].roomNumY;
            bool m_isRoomClear = _roomJsonDatasList[i].isRoomClear;
            Vector2 m_roomloca = loca[m_roomNumX, m_roomNumY];
            Vector2 m_roomPos = CameraManager.Instance.cam.WorldToScreenPoint(loca[m_roomNumX, m_roomNumY]);
            m_room.AdaptRoomData(_roomJsonDatasList[i], m_roomloca, m_roomPos, m_isRoomClear);
        }
        yield return null;
    }

    private void Awake()
    {
        //roomReset();
        //roomInfoActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //SceneLoadedFunc();
    }

    // Update is called once per frame
    void Update()
    {
        ExploreSetting();
        CursorUpdate();
        roomInfo();
    }
}
