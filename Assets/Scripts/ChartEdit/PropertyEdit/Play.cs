using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour
{
    public Button thisButton;
    private void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() =>
        {
            if (!StateManager.Instance.IsPlaying && !StateManager.Instance.IsStart)
            {
                StateManager.Instance.IsStart = true;
            }
            StateManager.Instance.IsPause = false;
        });
    }
}
