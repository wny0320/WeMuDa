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
    /// ���� ��ó�� �ִٸ� �������� ���� ������ �������ִ� �Լ�
    /// </summary>
    public void RandomRoomClicked()
    {
        if (ExploreMapManager.Instance.IsNear(roomNum))
        {
            int dice = Random.Range(0, 7);
            roomInformation = ExploreMapManager.Instance.RoomInformationList[dice];
            roomType = (int)(ExploreMapManager.RoomKind)dice;
            //ExploreMapManager.Instance.UpdateNowXY(roomNum[0],roomNum[1]); �̵����� �ʰ� ������ Ȯ���ϰ� ����
            revealedImage.sprite = ExploreMapManager.Instance.RoomInformationList[dice].RoomSprite;
            revealedFrameImage.color = new Color(1, 0, 0, 1);
            unrevealed.SetActive(false);
            revealed.SetActive(true);
            //RoomEvent(); �̵��� ���� �����μ� �� �̺�Ʈ�� �̵����� �߰��ϴ� ������ ����
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
                // �� �̺�Ʈ
                // �� �̺�Ʈ Ŭ���� �ÿ��� true�� �ٲ��ָ� �� ��?
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
        //���� �� Ÿ���� �������� ���� ���, �� Ž������ ���� ���
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
