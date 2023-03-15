using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blophy.Chart;
using UnityEngine.UI;
using Newtonsoft.Json;

public class WebManager : MonoBehaviourSingleton<WebManager>
{
    public string chartDataPath;
    public ChartData chartData;
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
        chartData = JsonConvert.DeserializeObject<ChartData>(Resources.Load<TextAsset>(chartDataPath).text);
        ChartData = chartData;
        MusicClip = musicClip;
        Background = background;
    }
}
