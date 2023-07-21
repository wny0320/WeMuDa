using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;
    public TMP_Text amountText;

    private void Awake()
    {
        amountText.gameObject.SetActive(false);
    }

    private Item _item;
    public Item item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;
            if (_item != null)
            {
                image.sprite = item.ItemSprite;
                image.color = new Color(1, 1, 1, 1);
            }
            else
                image.color = new Color(1, 1, 1, 0);
        }
    }
}
