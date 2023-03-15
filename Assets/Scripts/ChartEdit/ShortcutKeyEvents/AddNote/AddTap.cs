using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTap : AddNote
{
    public static AddTap Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = GetComponent<AddTap>();
        }
        else Destroy(this);
    }
    public void AddNote()
    {
        for (int i = 0; i < ChartPreviewEdit.Instance.noteLines.Count; i++)
        {
            if (!ChartPreviewEdit.Instance.noteLines[i].gameObject.activeInHierarchy) continue;

        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
