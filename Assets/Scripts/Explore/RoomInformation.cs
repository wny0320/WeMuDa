using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu]
public class RoomInformation : ScriptableObject
{
    public int RoomType;
    public Sprite RoomSprite;
    public string RoomName;
    public string RoomExplain;
}
