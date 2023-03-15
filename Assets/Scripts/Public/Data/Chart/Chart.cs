using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blophy.ChartEdit;
public class Chart : MonoBehaviourSingleton<Chart>
{
    public ChartData chartData;
    [Header("下面是制谱器专属的格式")]
    public List<Blophy.ChartEdit.Box> boxesEdit;
    public List<Blophy.ChartEdit.Text> textEdit;
    public void RefreshPlayer() => ProgressManager.Instance.OffsetTime(0);
    public void RefreshChart() => ChartPreviewEdit.DestoryAllBeatLines();
    public void Refresh()
    {
        Instance.RefreshPlayer();
        Instance.RefreshChart();
    }
}
