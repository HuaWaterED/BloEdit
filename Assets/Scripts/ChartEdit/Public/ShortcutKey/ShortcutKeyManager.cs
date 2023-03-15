using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutKeyManager : MonoBehaviourSingleton<ShortcutKeyManager>
{

    public ShortcutKey shortcutKey;
    public List<ShortcutKey> shortcutKeys = new();
    private void Start()
    {
        Instantiate(shortcutKey).Init(KeyCode.Q, Air, Air, () => AddTap.Instance.AddNote());
    }
    // Update is called once per frame
    void Update()
    {
    }
    void Air() { }
}