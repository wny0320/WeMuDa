using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using System.IO;

public class ExploreMapManager : MonoBehaviour
{
    public enum RoomKind
    {
        //Ž�� ���� ����
        start = -1,
        //�ļ��� ��ų� ���ø� �� �� �ִ� ���� ȣ�� ��
        lake,
        //�������� ������ ���̴� ��
        battle,
        //������ �ٸ� ���̽� ķ���� �ִ� ��
        baseCamp,
        //�������� ��
        mine,
        //���� ��
        trap,
        //������ �������� �ִ� ��
        item,
        //���� �ٸ� ���ΰ� �ִ� ��
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
    [SerializeField]
    private Vector2[,] loca;
    [SerializeField]
    private bool[,] locaFlag;

    private Vector2 startPos;
    [SerializeField]
    private GameObject startPoint;

    [SerializeField]
    private GameObject roomPrefab;
    [SerializeField]
    private GameObject mapCanvas;
    [SerializeField]
    private GameObject cursorPrefab;
    [SerializeField]
    private GameObject cursor;

    private int mapSize = 0;
    [SerializeField]
    private int maxMapSize;

    private int nowX;
    private int nowY;

    //roomInfo ���� field
    public List<RoomInformation> RoomInformationList;
    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEvent;

    private GameObject targetRoom = null;
    [SerializeField]
    private Image roomInfoImage;
    [SerializeField]
    private TMP_Text roomName;
    [SerializeField]
    private TMP_Text roomExplain;
    [SerializeField]
    private GameObject roomInfoObject;
    [SerializeField]
    private Sprite exploreSprite;
    private float roomNowTime = 0f;
    [SerializeField, Tooltip("���� ������ ���µ� �ɸ��� �ð�"), Range(0, 1)]
    private float roomMaxTime = 1f;

    private void ExploreSetting()
    {

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
        startPoint.GetComponent<Room>().StartRoomSet(loca[1, 5], startPos, new int[] { 1, 5 });
        startPoint.transform.SetParent(mapCanvas.transform);
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
        cursor.transform.SetParent(mapCanvas.transform);
        cursor.transform.position = new Vector2(startPos.x + 50f, startPos.y + 50f);
    }
    private void CursorUpdate()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene) return;
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
                //�迭 ���� ������ �Ǵ�
                if (IsOutOfRange(i, j))
                    continue;
                //���� ��ġ�� ���� ��ġ�� ���
                if (i == _x || i == _y)
                    continue;
                //���� ������ġ�� �̹� ���� �ִ� ���
                if (locaFlag[i, j])
                    continue;

                int _cnt = 0;
                //�迭 �������� �����¿쿡 ���� �ִ� ���(������ ���� �ִ°��)���� �Ǵ�
                //���� ������� �ʴٸ� cnt++
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
                //cnt�� 0�� ���, �� ������ ���� ���� ��� continue
                if (_cnt == 0)
                    continue;

                //���� ���ٸ� 50% Ȯ���� ���� ����
                bool _dice = Random.value > .5;
                if (_dice)
                {
                    GameObject _target = Instantiate(roomPrefab);
                    _target.transform.SetParent(mapCanvas.transform);
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
        //x�� �迭 ���� ��
        if (loca.GetLength(0) - 1 <= _x || _x < 1)
            return true;
        //y�� �迭 ���� ��
        if (loca.GetLength(1) - 1 <= _y || _y < 1)
            return true;
        return false;
    }
    /// <summary>
    /// ���� ��ġ �������� _roomNum�� �ش��ϴ� ��ǥ�� ���� �����ߴ����� bool�� ��ȯ
    /// </summary>
    /// <param name="_roomNum">�� ��ǥ loca��</param>
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
    private void roomReset()
    {
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEvent = new PointerEventData(null);
    }
    private void roomInfoActive(bool _tf)
    {
        roomInfoObject.SetActive(_tf);
    }
    /// <summary>
    /// ���콺�� �濡 1�ʰ� �θ� ���� ������ ����ִ� �Լ�
    /// </summary>
    private void roomInfo()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene) return;
        pointerEvent.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(pointerEvent, results);
        if (results.Count > 0)
        {
            if (targetRoom == results[0].gameObject && results[0].gameObject.TryGetComponent<Image>(out Image _image))
            {
                if (roomNowTime >= roomMaxTime && _image.sprite != null)
                {
                    //������ �����
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
        mapCanvas.SetActive(_tf);
    }
    public void StartExplore(List<GameObject> _minerList)
    {

    }
    public void BattleModeToExploreMode()
    {
        BattleManager.Instance.SetBattleCanvas(false);
        SetExploreCanvas(true);
    }
    public void SceneLoadedFunc()
    {
        if (GameSceneManager.Instance.NowSceneName != GameSceneManager.SceneName.ExploreScene) return;
        if (GameSceneManager.Instance.IsExploreSceneLoaded == true) return; // ������ �ε�� ���� �ִٸ� �Լ� ����
        GetSize();
        SetStartPoint();
        MapMaking(nowX, nowY);
        SetCursor();
        BackgroundManager.Instance.SetBackground(exploreSprite);
    }
    public void RoomDataExport()
    {
        Room[] roomArray = mapCanvas.GetComponentsInChildren<Room>();
        List<RoomJsonData> roomJsonDatasList = new List<RoomJsonData>();
        int roomCount = roomArray.Length;
        for (int i = 0; i < roomCount; i++)
        {
            RoomJsonData m_roomJsonData = roomArray[i].ReturnRoomJsonData();
            //���� ���� ��� ��ŵ
            if (m_roomJsonData.roomType == -1) continue;
            roomJsonDatasList.Add(m_roomJsonData);
        }
        string json = JsonConvert.SerializeObject(roomJsonDatasList);
        string path = Path.Combine(Application.persistentDataPath, "RoomData.json");
        File.WriteAllText(path, json);
        //Application.dataPath; �����Ϳ����� ������ ������ ���� ��ġ
        //Application.streamingAssetsPath; �б⸸ ������ ���� ��ġ
        //Application.persistentDataPath; �б� ���Ⱑ ��� ������ ���� ��ġ
        //�ƴϸ� PlayerPrefs�� ���� ����
    }
    public void RoomDataImport()
    {
        List<RoomJsonData> roomJsonDatasList = new List<RoomJsonData>();
        string path = Path.Combine(Application.persistentDataPath, "RoomData.json");
        if (File.Exists(path) == false) Debug.LogError("����� json������ �������� �ʽ��ϴ�!");
        string json = File.ReadAllText(path);
        roomJsonDatasList = JsonConvert.DeserializeObject<List<RoomJsonData>>(json);
        InstantiateRoomFromJson(roomJsonDatasList);
    }
    public void InstantiateRoomFromJson(List<RoomJsonData> _roomJsonDatasList)
    {
        //���� ���� ���������Ƿ� �ٽ� ����
        //Ŀ�� �������� ��
        SetStartPoint();

        int count = _roomJsonDatasList.Count;
        for(int i = 0; i < count; i++)
        {
            Room m_room = Instantiate(roomPrefab).GetComponent<Room>();
            m_room.transform.SetParent(mapCanvas.transform);
            int m_roomNumX = _roomJsonDatasList[i].roomNumX;
            int m_roomNumY = _roomJsonDatasList[i].roomNumY;
            Vector2 m_roomloca = loca[m_roomNumX, m_roomNumY];
            Vector2 m_roomPos = CameraManager.Instance.cam.WorldToScreenPoint(loca[m_roomNumX, m_roomNumY]);
            m_room.AdaptRoomData(_roomJsonDatasList[i], m_roomloca, m_roomPos);
        }
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
        CursorUpdate();
        roomInfo();
    }
}
