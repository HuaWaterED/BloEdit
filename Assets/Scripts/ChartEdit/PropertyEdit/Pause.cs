using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button thisButton;
    private void Start()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(() =>
        {
            StateManager.Instance.IsPause = true;



        });
    }
}
