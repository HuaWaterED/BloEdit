using Blophy.ChartEdit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEdit : MonoBehaviour
{
    public RectTransform rectTransform;
    public Blophy.ChartEdit.Event thisEvent;
    public bool isRefresh = false;

    public EventEdit Init(BeatLine beatLine, VLine vline, Public_LineDiv public_LineDiv)
    {
        BPMTime bpmTime = new() { integer = beatLine.thisBPM.integer, denominator = beatLine.thisBPM.denominator, molecule = beatLine.thisBPM.molecule };
        return Init(bpmTime, vline.positionX, public_LineDiv);
    }
    public EventEdit IsRefresh()
    {
        isRefresh = true;
        return this;
    }
    public virtual EventEdit Init(BPMTime bpmTime, float positionX, Public_LineDiv public_LineDiv)
    {
        Blophy.ChartEdit.Event tempEvent = thisEvent;

        thisEvent = new();
        if (isRefresh)
        {
            isRefresh = false;
            //thisNote.positionX = tempNote.positionX;
            //thisNote.hitTime = tempNote.hitTime;
            //thisNote.effect = tempNote.effect;
            //thisNote.endTime = tempNote.endTime;
            thisEvent.startTime = tempEvent.startTime;
            thisEvent.endTime = tempEvent.endTime;
            thisEvent.startValue = tempEvent.startValue;
            thisEvent.endValue = tempEvent.endValue;
            thisEvent.curve = tempEvent.curve;
        }
        else
        {
            //thisNote.positionX = positionX;
            //thisNote.hitTime = new(bpmTime.integer, bpmTime.molecule, bpmTime.denominator);
            //thisNote.effect = Blophy.Chart.NoteEffect.Ripple & Blophy.Chart.NoteEffect.CommonEffect;
            thisEvent.startTime = bpmTime;
            Public_AnimationCurveEaseEnum.keyValuePairs.TryGetValue(1, out AnimationCurve curve);
            thisEvent.curve = curve;
        }
        HoldLengthHandle();
        float canvasLocalPositionX = (public_LineDiv.vLines.edgeRightVerticalLine.transform.localPosition - public_LineDiv.vLines.edgeLeftVerticalLine.transform.localPosition).x / 2 * positionX;
        float canvasLocalPositionY = YScale.Instance.GetPositionYWithSecondsTime(BPMManager.Instance.GetSecondsTimeWithBPMSeconds(thisEvent.startTime.thisStartBPM));
        transform.localPosition = new Vector2(canvasLocalPositionX, canvasLocalPositionY);
        return this;
    }
    public virtual void HoldLengthHandle()
    {
        thisEvent.endTime = thisEvent.startTime;
    }
}
