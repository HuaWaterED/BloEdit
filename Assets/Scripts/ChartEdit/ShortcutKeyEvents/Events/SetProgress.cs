using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetProgress : ShortcutKeyEvent
{

    public override void ExeDown()
    {
        Debug.Log($"ExeSetProgress");
    }
    private void Update()
    {
        float delta = Input.GetAxis("Mouse ScrollWheel");
        if (delta == 0)
        {
            return;
        }
        ProgressManager.Instance.OffsetTime(delta);
        Chart.Instance.RefreshChart();
    }
}
