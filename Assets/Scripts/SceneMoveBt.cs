using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMoveBt : MonoBehaviour
{
    private Button sceneMoveBt;
    [SerializeField]
    private GameSceneManager.SceneName targetSceneName;
    private void Start()
    {
        sceneMoveBt = GetComponent<Button>();
        sceneMoveBt.onClick.AddListener(() =>
        {
            GameSceneManager.Instance.SceneMove(targetSceneName.ToString());
        });
    }
}
