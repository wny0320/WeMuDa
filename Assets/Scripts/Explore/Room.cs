using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Room : MonoBehaviour
{
    [SerializeField]
    GameObject unrevealed;
    [SerializeField]
    GameObject revealed;
    [SerializeField]
    Image revealedImage;

    [SerializeField]
    RoomInformation roomInformation;
    [SerializeField]
    int roomType;
    [SerializeField]
    Sprite[] roomTypeSprite;

    [SerializeField]
    Vector2 roomPos;
    [SerializeField]
    int[] roomNum;
    [SerializeField]
    bool isRoomClear = false;
    [SerializeField]
    Image revealedFrameImage;

    /// <summary>
    /// 방이 근처에 있다면 랜덤으로 방의 종류를 결정해주는 함수
    /// </summary>
    public void RandomRoomClicked()
    {
        if (ExploreMapManager.Instance.IsNear(roomNum))
        {
            int dice = Random.Range(0, 7);
            roomInformation = ExploreMapManager.Instance.RoomInformationList[dice];
            roomType = (int)(ExploreMapManager.RoomKind)dice;
            //ExploreMapManager.Instance.UpdateNowXY(roomNum[0],roomNum[1]); 이동하지 않고 정보만 확인하게 수정
            revealedImage.sprite = ExploreMapManager.Instance.RoomInformationList[dice].RoomSprite;
            revealedFrameImage.color = new Color(1, 0, 0, 1);
            unrevealed.SetActive(false);
            revealed.SetActive(true);
            //RoomEvent(); 이동을 따로 뺌으로서 룸 이벤트를 이동에다 추가하는 것으로 변경
        }
    }
    public void RoomEvent()
    {
        switch(roomType)
        {
            case (int)ExploreMapManager.RoomKind.lake:
                break;
            case (int)ExploreMapManager.RoomKind.battle:
                BattleManager.Instance.IsBattleStart = true;
                break;
            case (int)ExploreMapManager.RoomKind.baseCamp:
                break;
            case (int)ExploreMapManager.RoomKind.mine:
                break;
            case (int)ExploreMapManager.RoomKind.trap:
                break;
            case (int)ExploreMapManager.RoomKind.item:
                break;
            case (int)ExploreMapManager.RoomKind.miner:
                break;
        }
    }
    public void JustMoveRoom()
    {
        if(ExploreMapManager.Instance.IsNear(roomNum))
        {
            if(isRoomClear == false)
            {
                // 룸 이벤트
                // 룸 이벤트 클리어 시에만 true로 바꿔주면 될 듯?
                isRoomClear = true;
                revealedFrameImage.color = new Color(1, 0, 0, 0);
            }
            ExploreMapManager.Instance.UpdateNowXY(roomNum[0], roomNum[1]);
        }
    }
    public void RoomInstiate(Vector2 _loca, Vector2 _pos, int[] _roomNum)
    {
        roomPos = _loca;
        transform.position = _pos;
        roomNum = _roomNum;
        revealed.SetActive(false);
    }
    public void StartRoomSet(Vector2 _loca, Vector2 _pos, int[] _roomNum)
    {
        RoomInstiate(_loca, _pos, _roomNum);
        roomType = -1;
        revealed.SetActive(true);
        unrevealed.SetActive(false);
    }
    public RoomInformation ReturnRoomInfo()
    {
        return roomInformation;
    }
    public RoomJsonData ReturnRoomJsonData()
    {
        RoomJsonData roomJsonData = new RoomJsonData();
        roomJsonData.isRoomClear = isRoomClear;
        roomJsonData.roomType = roomType;
        roomJsonData.roomNumX = roomNum[0];
        roomJsonData.roomNumY = roomNum[1];
        return roomJsonData;
    }
    public void AdaptRoomData(RoomJsonData _roomJsonData, Vector2 _loca, Vector2 _pos)
    {
        roomPos = _loca;
        transform.position = _pos;
        roomNum = new int[] { _roomJsonData.roomNumX, _roomJsonData.roomNumY };
        //아직 룸 타입이 정해지지 않은 경우, 즉 탐색하지 않은 경우
        int m_roomType = _roomJsonData.roomType;
        if(m_roomType == -1) revealed.SetActive(false);
        else
        {
            roomInformation = ExploreMapManager.Instance.RoomInformationList[m_roomType];
            roomType = (int)(ExploreMapManager.RoomKind)m_roomType;
            revealedImage.sprite = ExploreMapManager.Instance.RoomInformationList[m_roomType].RoomSprite;
            revealedFrameImage.color = new Color(1, 0, 0, 1);
            unrevealed.SetActive(false);
            revealed.SetActive(true);
        }
    }
}
