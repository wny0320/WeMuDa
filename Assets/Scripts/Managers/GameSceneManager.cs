using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

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
            SceneManager.LoadScene(SceneName.ExploreScene.ToString());
            ExploreMapManager.Instance.RoomDataImport();
        }
        else
        {
            // Scene 전환
            SceneManager.LoadScene(SceneName.ExploreScene.ToString());
        }
        IsSceneLoaded = true;
    }
    public void ChangeToCampScene()
    {
        IsExploreSceneLoaded = true;
        ExploreMapManager.Instance.RoomDataExport();
        SceneManager.LoadScene(SceneName.CampScene.ToString());
        IsSceneLoaded = true;
    }
    public void SceneMove(string _name)
    {
        SceneManager.LoadScene(_name);
        IsSceneLoaded = true;
    }
    private void GetSceneName()
    {
        string strSceneName = SceneManager.GetActiveScene().name;
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
    }
    private void LateUpdate()
    {
        IsSceneLoaded = false;
    }
    private void Update()
    {
        GetSceneName();
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
