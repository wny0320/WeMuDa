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
        PopupManager.Instance.ShowMessage(new cPopup("���̵� ����", "������ �÷����� ���̵��� �������ּ���.", () => { canLevelSelect = true; }));
        StartCoroutine(setLevel());
    }
    /// <summary>
    /// �÷��̾�� ���̵��� ��� �� �ش� ���̵��� ��ȯ���ִ� �Լ�
    /// </summary>
    /// <returns>�÷��̾ ������ ���̵�, Level ������ �� ��</returns>
    private IEnumerator setLevel()
    {
        //���̵� ���� UI Ȱ��ȭ
        while(canLevelSelect == false || Level == LevelEnum.nonSelect)
        {
            yield return null;
        }
        //���̵� ���� UI ��Ȱ��ȭ
        yield break;
    }
}
