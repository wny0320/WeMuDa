using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Scene ��ȯ�� �� Scene�� �̸����� ��ȯ�Ǹ� �ش� Scene�� Enum���� ����, Scene ��ȯ �̺�Ʈ�� bool�� Flag�� ����
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
    public bool IsSceneNameChanged = false; // Scene�� ��ȯ�Ǵ� ������ Flag�� �ٲ���µ� Scene�� �ٲ�µ� ���� �ɷ��� ���� bool

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
            // ExploreScene�� �ε�� ���� �ִٸ� �ʻ����� ���� �ʰ� Json������ ��������
            //SceneManager.LoadScene(SceneName.ExploreScene.ToString());
            ExploreMapManager.Instance.RoomDataImport();
        }
        else
        {
            // Scene ��ȯ
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
        
        RoomInformation�� String���� �����ؼ� �ٽ� roomKind���� ã�Ƽ� �־��ָ� �Ǳ� ������ �����ϰ�
        �װ� �ƴϴ��� Roomtype�� ���� ������ ������ ������ �� ����
        RoomPos�� ������ �־�� �ϸ�
        RoomNum�� ���� ������ Json���� ���ٰ� �ٽ� �� �ҷ��� �� �����ϴ� ������� �� �� ����
        �Ƹ� �� �Ŵ��� �ϳ� ���� ����ϸ� �� �� ��
     */
}
