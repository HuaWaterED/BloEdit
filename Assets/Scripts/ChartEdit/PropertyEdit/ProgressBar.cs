using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviourSingleton<ProgressBar>
{
    public Scrollbar progressBar;
    private void Start()
    {
        progressBar = GetComponent<Scrollbar>();
        progressBar.onValueChanged.AddListener((theValue) =>
        {
            float result = Chart.Instance.chartData.globalData.musicLength * theValue;
            ProgressManager.Instance.SetTime(result);
            //for (int i = 0; i < ChartPreviewEdit.Instance.lines.Count; i++)
            //{
            //    ChartPreviewEdit.Instance.lines[i].ResetLastBPM(BPMManager.Instance.GetBPMWithSecondsTime(((float)ProgressManager.Instance.CurrentTime)));
            //}
            Chart.Instance.Refresh();
        });
    }
    private void Update()
    {
        float currentProgress = (float)ProgressManager.Instance.CurrentTime / Chart.Instance.chartData.globalData.musicLength;
        progressBar.SetValueWithoutNotify(currentProgress);
        if (currentProgress >= .9999f)
        {
            StateManager.RestartTime(On_Off.Instance.isContinuePlay.isOn);
            for (int i = 0; i < ChartPreviewEdit.Instance.lines.Count; i++)
            {
                ChartPreviewEdit.Instance.lines[i].ResetLastBPM(BPMManager.Instance.GetBPMWithSecondsTime(((float)ProgressManager.Instance.CurrentTime)));
            }
        }
    }
}
