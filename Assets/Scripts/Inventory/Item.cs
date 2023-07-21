using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string ItemName;
    public Sprite ItemSprite;
    public string ItemExplain;

    // 아이템의 제작법, null일 경우에는 제작 불가 아이템
    // 재료 아이템 Item(Key), 아이템의 수량 int(Value)
    // 사용법 public SerializableDictionary<Item, int> MaterialItems;
    // https://uwostudy.tistory.com/84 참고하에 패키지를 사용함
    public SerializableDictionary<Item, int> MaterialItems;
}
