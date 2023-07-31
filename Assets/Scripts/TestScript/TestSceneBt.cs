using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSceneBt : MonoBehaviour
{
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener (() =>
        {
            GameSceneManager.Instance.ChangeFromExploreScene();
        });
    }
}
