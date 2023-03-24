using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutKeyManager : MonoBehaviourSingleton<ShortcutKeyManager>
{

    public ShortcutKey shortcutKey;//快捷键预制件
    public List<ShortcutKey> shortcutKeys = new();//召唤出来的预制件的列表
    public List<ShortcutKeyEvent> shortcutEventList = new();//事件列表
    public List<ShortcutKeyTable> shortcutKeyMap = new();
    private IEnumerator Start()
    {
        while (true)
        {
            ReloadShortcutKey();
            yield return new WaitForSeconds(5);
        }
    }

    private void ReloadShortcutKey()
    {
        int count = shortcutKeys.Count;
        for (int i = 0; i < count; i++)
        {
            ShortcutKey shortcutKey = shortcutKeys[0];
            shortcutKeys.Remove(shortcutKey);
            Destroy(shortcutKey.gameObject);
        }
        for (int i = 0; i < shortcutKeyMap.Count; i++)
        {
            ShortcutKeyEvent @event = null;
            for (int j = 0; j < shortcutEventList.Count; j++)
            {
                if (shortcutEventList[i].eventID == shortcutKeyMap[i].eventID)
                {
                    @event = shortcutEventList[i];
                    break;
                }
            }
            ShortcutKey instShortcutKey =
                Instantiate(shortcutKey, transform)
                .Init(shortcutKeyMap[i].keyCode, shortcutKeyMap[i].keyCode2, shortcutKeyMap[i].isDoublePress, @event.ExeDown, @event.Exe, @event.ExeUp);
            shortcutKeys.Add(instShortcutKey);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
[Serializable]
public class ShortcutKeyTable
{
    public KeyCode keyCode;
    public KeyCode keyCode2;
    public bool isDoublePress;
    public string eventID;
}