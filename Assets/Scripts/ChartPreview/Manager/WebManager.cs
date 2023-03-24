using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blophy.Chart;
using UnityEngine.UI;
using Newtonsoft.Json;

public class WebManager : MonoBehaviourSingleton<WebManager>
{
    public string chartDataPath;
    public AudioClip musicClip;
    public Image background;
    public ChartData ChartData
    {
        get => Chart.Instance.chartData;
        set => Chart.Instance.chartData = value;
    }
    public AudioClip MusicClip
    {
        get => AssetManager.Instance.musicPlayer.clip;
        set => AssetManager.Instance.musicPlayer.clip = value;
    }
    public Image Background
    {
        get => AssetManager.Instance.background;
        set => AssetManager.Instance.background = value;
    }
    private void Start()
    {
        //chartData = JsonConvert.DeserializeObject<ChartData>(Resources.Load<TextAsset>(chartDataPath).text);
        //chartData = 
        MusicClip = musicClip;
        Background = background;
        ChartData = ChartTools.CreateNew();
        Chart.Instance.boxesEdit = new();
        Chart.Instance.boxesEdit.Add(new());
        Chart.Instance.boxesEdit[0].lines = new() { new(), new(), new(), new(), new() };
        Chart.Instance.boxesEdit[0].boxEvents = new();
        Chart.Instance.boxesEdit[0].boxEvents.scaleX = new();
        Chart.Instance.boxesEdit[0].boxEvents.scaleY = new();
        Chart.Instance.boxesEdit[0].boxEvents.moveX = new();
        Chart.Instance.boxesEdit[0].boxEvents.moveY = new();
        Chart.Instance.boxesEdit[0].boxEvents.centerX = new();
        Chart.Instance.boxesEdit[0].boxEvents.centerY = new();
        Chart.Instance.boxesEdit[0].boxEvents.alpha = new();
        Chart.Instance.boxesEdit[0].boxEvents.lineAlpha = new();
        Chart.Instance.boxesEdit[0].boxEvents.rotate = new();
        for (int i = 0; i < Chart.Instance.boxesEdit[0].lines.Count; i++)
        {
            Chart.Instance.boxesEdit[0].lines[i].offlineNotes = new();
            Chart.Instance.boxesEdit[0].lines[i].onlineNotes = new();
            Chart.Instance.boxesEdit[0].lines[i].speed = new();
        }
    }
}
