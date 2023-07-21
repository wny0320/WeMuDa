using System.Collections.Generic;
public struct AbilityStruct
{
    public List<PerAbilEnum> MinerPerfectAbilityList;
    public List<PosAbilEnum> MinerPositiveAbilityList;
    public List<NegAbilEnum> MinerNegativeAbilityList;
}
public struct RoomJsonData
{
    public bool isRoomClear;
    public int roomType;
    public int roomNumY;
    public int roomNumX;
}
