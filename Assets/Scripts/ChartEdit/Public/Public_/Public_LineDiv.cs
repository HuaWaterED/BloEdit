using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Public_LineDiv : MonoBehaviour
{
    public Transform arisePosition;
    public BeatLine beatLine;
    public List<BeatLine> beatLines = new();
    public BPM lastBPM;
    public float ariseLineAndLinePositionYDelta;
    public float ariseLineAndLineSeconds => ariseLineAndLinePositionYDelta / 100;
    public float CurrentBasicLine => YScale.Instance.GetPositionYWithSecondsTime((float)ProgressManager.Instance.CurrentTime);
    public float CurrentAriseLine => YScale.Instance.GetPositionYWithSecondsTime((float)ProgressManager.Instance.CurrentTime) + ariseLineAndLinePositionYDelta;

    public int lastAriseBeatLineIndex = 0;
    private void Update()
    {
        transform.localPosition = Vector2.down * CurrentBasicLine;
        arisePosition.localPosition = Vector2.up * CurrentAriseLine;
        float ariseBPMSeconds = BPMManager.Instance.GetBPMSecondsWithSecondsTime((float)(ProgressManager.Instance.CurrentTime + ariseLineAndLineSeconds * (1 / YScale.Instance.CurrentYScale)));
        int ariseBeatLineIndex = Algorithm.BinarySearch(BPMManager.Instance.bpmList, m => m.thisStartBPM < ariseBPMSeconds, false);
        while (lastBPM.thisStartBPM < ariseBPMSeconds)
        {
            BeatLine initBeatLine = Instantiate(beatLine, transform).Init(lastBPM.thisStartBPM, lastBPM);
            beatLines.Add(initBeatLine);
            lastBPM.AddOneBeat();
        }
        float BPMSeconds = BPMManager.Instance.GetBPMSecondsWithSecondsTime((float)ProgressManager.Instance.CurrentTime);
        for (int i = 0; i < beatLines.Count; i++)
        {
            while (beatLines[i].thisBPM.thisStartBPM < BPMSeconds)
            {
                BeatLine thisBeatLine = beatLines[i];
                beatLines.Remove(thisBeatLine);
                Destroy(thisBeatLine.gameObject);
            }
            while (beatLines[i].thisBPM.thisStartBPM > ariseBPMSeconds)
            {
                BeatLine thisBeatLine = beatLines[i];
                beatLines.Remove(thisBeatLine);
                Destroy(thisBeatLine.gameObject);
            }
        }
    }
    public void ResetLastBPM(int integer = 0, int molecule = 0, int denominator = 1)
    {
        lastBPM.integer = integer;
        lastBPM.molecule = molecule;
        lastBPM.denominator = denominator;
    }
    public void ResetLastBPM(BPM bpm)
    {
        lastBPM = new(bpm);
    }
}
