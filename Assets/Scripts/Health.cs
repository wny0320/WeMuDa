using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // ��Ʈ����, ���, ������ �ִ� ��
    private float maxStress;
    private float maxHungry;
    private float maxThirsty;

    // ��Ʈ����, ���, ������ ���� ��
    private float stress;
    private float hungry;
    private float thirsty;

    // ���� �Ѱ�ġ �̻��� �� ��
    private int stressOver;
    private int hungryOver;
    private int thirstyOver;

    private const int overCount = 3;

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
