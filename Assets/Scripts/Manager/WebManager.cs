using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blophy.Chart;
using UnityEngine.UI;

public class WebManager : MonoBehaviourSingleton<WebManager>
{
    public ChartData ChartData
    {
        get => AssetManager.Instance.chartData;
        set
        {
            AssetManager.Instance.chartData = value;
            UIManager.Instance.musicName.text = value.metaData.musicName;
            UIManager.Instance.level.text = value.metaData.chartLevel;
            TextManager.Instance.Init(value.texts);
        }

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
}
