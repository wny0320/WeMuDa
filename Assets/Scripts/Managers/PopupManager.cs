using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PopupManager>().GetComponent<PopupManager>();
                if(instance = null)
                {
                    GameObject gameObject = new GameObject();
                    instance = gameObject.AddComponent<PopupManager>();
                    gameObject.name = typeof(PopupManager).ToString();
                }
            }
            return instance;
        }
    }
    private static PopupManager instance;
    [SerializeField] private GameObject m_objPopup;
    [SerializeField] private TextMeshProUGUI m_textTitle;
    [SerializeField] private TextMeshProUGUI m_textValue;
    [SerializeField] private Image m_imgPopup;
    [SerializeField] private Button m_btnPopup;
    [SerializeField] private Image m_btnImage;

    private List<cPopup> m_listMessages = new List<cPopup>();
    [Tooltip("꺼지는 시간을 조절해 줍니다.")]public float m_fCloseTime = 1.0f;


    // Start is called before the first frame update
    private void Start()
    {
        init();
    }

    private void init()
    {
        m_objPopup.SetActive(false);
        m_textTitle.text = string.Empty;
        m_textValue.text = string.Empty;
        setTextAlpha(m_textTitle, 1.0f);
        setTextAlpha(m_textValue, 1.0f);
        setImageAlpha(m_imgPopup, 1.0f);
        setImageAlpha(m_btnImage, 1.0f);
        m_btnPopup.onClick.RemoveAllListeners();
        m_btnPopup.onClick.AddListener(() => 
        {
            StartCoroutine(close());
        });
    }

    private void setTextAlpha(TextMeshProUGUI _text, float _alpha)
    {
        _alpha = Mathf.Clamp(_alpha, 0f, 1f);
        Color color = _text.color;
        color.a = _alpha;
        _text.color = color;
    }
    private void setImageAlpha(Image _img, float _alpha)
    {
        _alpha = Mathf.Clamp(_alpha, 0f, 1f);
        Color color = _img.color;
        color.a = _alpha;
        _img.color = color;
    }

    public void ShowMessage(cPopup _value)
    {
        m_listMessages.Add(_value);
        showMessage();
    }

    private void showMessage()
    {
        if (m_listMessages.Count == 0)
        {
            return;
        }

        cPopup current = m_listMessages[0];

        m_textTitle.text = current.Title;
        m_textValue.text = current.Value;

        setTextAlpha(m_textTitle, 1.0f);//글자
        setTextAlpha(m_textValue, 1.0f);//글자
        setImageAlpha(m_imgPopup, 1.0f);//이미지
        setImageAlpha(m_btnImage, 1.0f);

        m_btnPopup.interactable = true;
        m_objPopup.SetActive(true);
    }

    IEnumerator close()
    {
        m_btnPopup.interactable = false;
        Color color = new Color(0, 0, 0, 1f);

        while (true)
        {
            m_textTitle.color -= color * Time.deltaTime * (1 / m_fCloseTime);
            m_textValue.color -= color * Time.deltaTime * (1 / m_fCloseTime);
            m_imgPopup.color -= color * Time.deltaTime * (1 / m_fCloseTime);
            m_btnImage.color -= color * Time.deltaTime * (1 / m_fCloseTime);

            if (m_imgPopup.color.a > 0)
            {
                yield return null;
            }
            else
            {
                break;
            }
        }

        if (m_listMessages[0].Action != null)
        {
            m_listMessages[0].Action.Invoke();
        }
        m_listMessages.RemoveAt(0);
        m_objPopup.SetActive(false);
        showMessage();
    }
}
