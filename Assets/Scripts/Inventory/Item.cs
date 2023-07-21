using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite ItemSprite;
    public string ItemExplain;

    // �������� ���۹�, null�� ��쿡�� ���� �Ұ� ������
    // ��� ������ Item(Key), �������� ���� int(Value)
    // ���� public SerializableDictionary<Item, int> MaterialItems;
    // https://uwostudy.tistory.com/84 �����Ͽ� ��Ű���� �����
    public SerializableDictionary<Item, int> MaterialItems;
}
