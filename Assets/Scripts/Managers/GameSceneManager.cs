using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Scene 전환할 때 Scene의 이름으로 전환되며 해당 Scene을 Enum으로 관리, Scene 전환 이벤트를 bool로 Flag를 받음
/// </summary>
public class GameSceneManager : MonoBehaviour
{
    public enum SceneName
    {
        Error,
        CampScene,
        ExploreScene,
        StoryScene,

    }
    public bool IsExploreSceneLoaded = false;
    public bool IsSceneLoaded = false;
    public bool IsGameStart = false;
    public bool IsSceneNameChanged = false; // Scene이 전환되는 과정에 Flag는 바뀌었는데 Scene은 바뀌는데 오래 걸려서 넣은 bool

    public SceneName NowSceneName;

    private static GameSceneManager instance;
    public static GameSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameSceneManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<GameSceneManager>();
                    obj.name = typeof(GameSceneManager).Name;
                }
            }
            return instance;
        }
    }

    public void ChangeToExploreScene()
    {
        if(IsExploreSceneLoaded == true)
        {
            // ExploreScene이 로드된 적이 있다면 맵생성을 하지 않고 Json파일을 가져오기
            //SceneManager.LoadScene(SceneName.ExploreScene.ToString());
            ExploreMapManager.Instance.RoomDataImport();
        }
        else
        {
            // Scene 전환
            //SceneManager.LoadScene(SceneName.ExploreScene.ToString());
        }
        //IsSceneLoaded = true;
    }
    public void ChangeFromExploreScene()
    {
        IsExploreSceneLoaded = true;
        ExploreMapManager.Instance.RoomDataExport();
        //SceneManager.LoadScene(SceneName.CampScene.ToString());
        //IsSceneLoaded = true;
    }
    public void SceneMove(string _name)
    {
        if (NowSceneName == SceneName.StoryScene && _name == SceneName.CampScene.ToString()) MinerManager.Instance.InitMiner();
        if (_name == SceneName.ExploreScene.ToString()) ChangeToExploreScene();
        if (NowSceneName == SceneName.ExploreScene) ChangeFromExploreScene();
        SceneManager.LoadScene(_name);
        IsSceneLoaded = true;
    }
    private void getSceneName()
    {
        string strSceneName = SceneManager.GetActiveScene().name;
        if(strSceneName == NowSceneName.ToString())
        {
            IsSceneNameChanged = false;
            return;
        }
        switch(strSceneName)
        {
            case "CampScene":
                NowSceneName = SceneName.CampScene;
                break;
            case "ExploreScene":
                NowSceneName = SceneName.ExploreScene;
                break;
            case "StoryScene":
                NowSceneName = SceneName.StoryScene;
                break;
            default:
                NowSceneName = SceneName.Error;
                break;
        }
        IsSceneNameChanged = true;
    }
    private void LateUpdate()
    {
        IsSceneLoaded = false;
    }
    private void Update()
    {
        getSceneName();
    }
    /*
        JsonConvert.SerializeObject();
        JsonConvert.DeserializeObject<>();

        JsonUtility.ToJson();
        JsonUtility.FromJson<>();
        
        RoomInformation을 String으로 저장해서 다시 roomKind에서 찾아서 넣어주면 되기 때문에 가능하고
        그게 아니더라도 Roomtype에 대한 정보가 있으면 가져올 수 있음
        RoomPos는 가지고 있어야 하며
        RoomNum에 대한 정보도 Json으로 뺐다가 다시 씬 불러올 때 적용하는 방식으로 할 수 있음
        아마 씬 매니저 하나 만들어서 사용하면 될 듯 함
     */
}
