using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField]
    [Tooltip("���� ���� �ɷ�ġ, ��������� ������ �ִ� �������� �����Ѵ�.")]
    float str;
    [SerializeField]
    [Tooltip("��ø�� ���� �ɷ�ġ, ��������� ������ ȸ������ �÷��ش�. ������ üũ�ϴ� ��ġ�̱⵵ �ϴ�.")]
    float agi;
    [SerializeField]
    [Tooltip("Ȱ�¿� ���� �ɷ�ġ, �κ��� ü�¿� ������ �ش�.")]
    float vit;
    [SerializeField]
    [Tooltip("���ֿ� ���� �ɷ�ġ, ���ۼҿ� ��ġ�� �κ��� ���� �ɷ�ġ��ŭ ���� Ȯ���� ����Ѵ�. ���� ��������� ������ ���߷��� �����Ѵ�.")]
    float dex;
    [SerializeField]
    [Tooltip("���ɿ� ���� �ɷ�ġ, ����� �߰��� ��� Ȯ���� �����Ѵ�.")]
    float @int;

    public void RandomStat()
    {
        //��� �ɷ�ġ�� 0~5 ���̷� ����
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
