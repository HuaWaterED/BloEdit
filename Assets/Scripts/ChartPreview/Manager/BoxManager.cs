using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviourSingleton<BoxManager>
{
    private void Start()
    {
        for (int i = 0; i < Chart.Instance.chartData.boxes.Count; i++)
        {
            Instantiate(AssetManager.Instance.boxController, AssetManager.Instance.box)
                .SetSortSeed(i * ValueManager.Instance.noteRendererOrder)//这里的3是每一层分为三小层，第一层是方框渲染层，第二和三层是音符渲染层，有些音符占用两个渲染层，例如Hold，FullFlick
                .Init(Chart.Instance.chartData.boxes[i]);
        }
        StateManager.Instance.IsStart = true;
        StateManager.Instance.IsPause = true;
    }
}
