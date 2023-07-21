using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField]
    [Tooltip("힘에 관한 능력치, 동물들과의 전투시 주는 데미지가 증가한다.")]
    float str;
    [SerializeField]
    [Tooltip("민첩에 관한 능력치, 동물들과의 전투시 회피율을 올려준다. 선공을 체크하는 수치이기도 하다.")]
    float agi;
    [SerializeField]
    [Tooltip("활력에 관한 능력치, 인부의 체력에 영향을 준다.")]
    float vit;
    [SerializeField]
    [Tooltip("재주에 관한 능력치, 제작소에 배치된 인부의 재주 능력치만큼 제작 확률이 상승한다. 또한 동물들과의 전투시 명중률이 증가한다.")]
    float dex;
    [SerializeField]
    [Tooltip("지능에 관한 능력치, 재능을 추가로 배울 확률이 증가한다.")]
    float @int;

    public void RandomStat()
    {
        //모든 능력치를 0~5 사이로 받음
        str = Random.Range(0, 5);
        agi = Random.Range(0, 5);
        vit = Random.Range(0, 5);
        dex = Random.Range(0, 5);
        @int = Random.Range(0, 5);
    }
    public float[] GetStat()
    {
        return new float[] { str, agi, vit, dex, @int };
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
