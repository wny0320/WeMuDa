using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public bool IsExploreSceneLoaded = false;
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
            SceneManager.LoadScene("ExploreScene");
            ExploreMapManager.Instance.RoomDataImport();
        }
        else
        {
            // Scene ��ȯ
            SceneManager.LoadScene("ExploreScene");
        }
    }
    public void ChangeToCampScene()
    {
        IsExploreSceneLoaded = true;
        ExploreMapManager.Instance.RoomDataExport();
        SceneManager.LoadScene("CampScene");
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
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
