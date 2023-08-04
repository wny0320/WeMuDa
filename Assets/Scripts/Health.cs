using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // 스트레스, 허기, 갈증의 최대 값
    private float maxStress;
    private float maxHungry;
    private float maxThirsty;

    // 스트레스, 허기, 갈증의 현재 값
    private float stress;
    private float hungry;
    private float thirsty;

    // 각각 한계치 이상이 된 수
    private int stressOver;
    private int hungryOver;
    private int thirstyOver;

    private const int overCount = 3;

    /// <summary>
    /// 게임속 하루가 지났을 경우 광부가 겪는 현상을 표현하는 함수
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
    public List<float> GetMaxHealth()
    {
        return new List<float> {maxStress, maxHungry, maxThirsty};
    }
    public List<float> GetHealth()
    {
        return new List<float> {stress, hungry, thirsty};
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
