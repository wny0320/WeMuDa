using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // ���� Range�� �� ������ ��� dynamic���� ���� ������Ʈ�� �ۼ��� ���� �ƴ϶�� Range�� �Ⱦ��°� ������
    [Header("Miner Health")]
    [SerializeField]
    [Tooltip("Stress Value"), Range(0,100)]
    float stress;
    [SerializeField]
    [Tooltip("Hungry Value"), Range(0, 100)]
    float hungry;
    [SerializeField]
    [Tooltip("Thirsty Value"), Range(0, 100)]
    float thirsty;
    [SerializeField]
    [Tooltip("Stress Over Value"), Range(0, 3)]
    int stressOver;
    [SerializeField]
    [Tooltip("Hungry Over Value"), Range(0, 3)]
    int hungryOver;
    [SerializeField]
    [Tooltip("Thirsty Over Value"), Range(0, 3)]
    int thirstyOver;

    /// <summary>
    /// ���Ӽ� �Ϸ簡 ������ ��� ���ΰ� �޴� ������ ǥ���ϴ� �Լ�
    /// </summary>
    public void OneDayAfter()
    {
        if (hungry >= 10)
        {
            hungry -= 10;
            hungryOver = 0;
        }
        else
        {
            hungryOver += 1;
            stress += 10;
        }

        if (thirsty >= 10)
        {
            thirsty -= 10;
            thirstyOver = 0;
        }
        else
        {
            thirstyOver += 1;
            stress += 10;
        }

        if (stress >= 100)
        {
            stressOver += 1;
        }
        else
        {
            stressOver = 0;
        }
    }
    public void HealthReset()
    {
        stress = 20.0f;
        hungry = 100.0f;
        thirsty = 100.0f;
        stressOver = 0;
        hungryOver = 0;
        thirstyOver = 0;
    }
    public float[] GetHealth()
    {
        return new float[] {stress, hungry, thirsty};
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
