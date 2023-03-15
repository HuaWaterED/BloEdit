using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutKey : MonoBehaviour
{
    public KeyCode keyCode;
    public Action keyDown;
    public Action keyHold;
    public Action keyUp;
    public ShortcutKey Init(KeyCode keyCode, Action keyDown, Action keyHold, Action keyUp)
    {
        this.keyCode = keyCode;
        this.keyDown = keyDown;
        this.keyHold = keyHold;
        this.keyUp = keyUp;
        return this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            keyDown();
        }
        if (Input.GetKey(keyCode))
        {
            keyHold();
        }
        if (Input.GetKeyUp(keyCode))
        {
            keyUp();
        }
    }
}
