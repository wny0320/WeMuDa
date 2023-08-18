using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinerInfoLRBt : MonoBehaviour
{
    private enum Direction
    {
        left,
        right,
    }
    private Button button;
    [SerializeField]
    private Direction direction;
    private void Awake()
    {
        button = GetComponent<Button>();
        MinerInfoManager info = MinerInfoManager.Instance;
        button.onClick.AddListener(() =>
        {
            if (direction == Direction.left) info.LeftButton();
            if (direction == Direction.right) info.RightButton();
        });
    }
}
