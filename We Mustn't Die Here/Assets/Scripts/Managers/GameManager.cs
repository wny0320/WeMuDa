using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum LevelEnum
    {
        nonSelect,
        easy,
        normal,
        hard,
        hardCore,
    }
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>().GetComponent<GameManager>();
                if (instance = null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<GameManager>();
                    gameObject.name = typeof(GameManager).ToString();
                }
            }
            return instance;
        }
    }
    [HideInInspector]
    public LevelEnum Level = LevelEnum.nonSelect;
    private bool canLevelSelect = false;

    public void Tutorial()
    {
        PopupManager.Instance.ShowMessage(new cPopup("난이도 선택", "게임을 플레이할 난이도를 선택해주세요.", () => { canLevelSelect = true; }));
        StartCoroutine(setLevel());
    }
    /// <summary>
    /// 플레이어에게 난이도를 물어본 뒤 해당 난이도를 반환해주는 함수
    /// </summary>
    /// <returns>플레이어가 선택한 난이도, Level 변수에 들어갈 값</returns>
    private IEnumerator setLevel()
    {
        //난이도 선택 UI 활성화
        while(canLevelSelect == false || Level == LevelEnum.nonSelect)
        {
            yield return null;
        }
        //난이도 선택 UI 비활성화
        yield break;
    }
}
