using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerManager : MonoBehaviour
{
    public List<Miner> MinerList = new List<Miner>();
    [SerializeField] List<Sprite> MinerSprite = new List<Sprite>();
    [SerializeField] GameObject MinerPrefab;
    static MinerManager instance;
    public static MinerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MinerManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<MinerManager>();
                    obj.name = typeof(MinerManager).Name;
                }
            }
            return instance;
        }
    }
    string[] LastNameList; //±¤ºÎ ÀÌ¸§ÀÇ ¼º
    string[] FirstNameList; //±¤ºÎ ÀÌ¸§ÀÇ ¼ºÀ» »« ³ª¸ÓÁö
    private void Awake()
    {
        LastNameList = new string[] {"±è","ÀÌ","¹Ú","°­","³²±Ã","°û","°í","±â","´ë","µ¶","·ù","ÀÓ","¼±","¼³","Ã¢","Àå","ÇÔ","Àü","¹é","À±","Çã"};
        FirstNameList = new string[] {"¼ºÈÆ","¼öºó","¹ÎÈ£","ÇýÁø","¹Î¿í","°­¹Î","»ó¹Î","Ã¶¼ö","¹ÎÀç","Ã¢¹Î","¹Î»ó","Áø¿ì","³ª¿µ","Á¤¿¬","Áö¿õ","Á¾Çõ","ÀåÇõ","ÈÆ", "¹Î"};
    }
    Sprite RandomSprite()
    {
        return MinerSprite[Random.Range(0, MinerSprite.Count - 1)];
    }

    string RandomName()
    {
        string minerName = LastNameList[Random.Range(0, LastNameList.Length - 1)] + FirstNameList[Random.Range(0, FirstNameList.Length - 1)];
        return minerName;
    }

    void InsantiateMiner()
    {
        Sprite sprite = RandomSprite();
        string name = RandomName();
        for (int i = 0; i < MinerList.Count; i++)
        {
            if (MinerList[i].gameObject.activeSelf == false)
            {
                MinerList[i].Spawn(name,sprite);
                return;
            }
        }

        GameObject obj = Instantiate(MinerPrefab);
        obj.name = "Miner";
        Miner miner = obj.GetComponent<Miner>();
        miner.Spawn(name,sprite);
        obj.transform.parent = transform;
        MinerList.Add(miner);
    }

    public void OneDayAfterMng()
    {
        for (int i = 0; i < MinerList.Count; i++)
        {
            if (MinerList[i].gameObject.activeSelf)
            {
                MinerList[i].CallOneDayAfter();
            }
        }
    }
}
