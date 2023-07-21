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
            // ExploreScene�� �ε�� ���� �ִٸ� �ʻ����� ���� �ʰ� Json������ ��������
            SceneManager.LoadScene(SceneName.ExploreScene.ToString());
            ExploreMapManager.Instance.RoomDataImport();
        }
        else
        {
            // Scene ��ȯ
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
        
        RoomInformation�� String���� �����ؼ� �ٽ� roomKind���� ã�Ƽ� �־��ָ� �Ǳ� ������ �����ϰ�
        �װ� �ƴϴ��� Roomtype�� ���� ������ ������ ������ �� ����
        RoomPos�� ������ �־�� �ϸ�
        RoomNum�� ���� ������ Json���� ���ٰ� �ٽ� �� �ҷ��� �� �����ϴ� ������� �� �� ����
        �Ƹ� �� �Ŵ��� �ϳ� ���� ����ϸ� �� �� ��
     */
}
