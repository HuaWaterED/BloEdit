using Blophy.ChartEdit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteEdit : MonoBehaviour
{
    //new Vector2(nearestVerticalLine.transform.position.x, nearestBeatLine.transform.position.y)
    public Note thisNote;
    public RectTransform rectTransform;
    public bool isRefresh = false;
    public NoteEdit IsRefresh()
    {
        isRefresh = true;
        return this;
    }
    public virtual NoteEdit Init(BeatLine beatLine, VLine vline, Public_LineDiv public_LineDiv)
    {
        BPMTime bpmTime = new() { integer = beatLine.thisBPM.integer, denominator = beatLine.thisBPM.denominator, molecule = beatLine.thisBPM.molecule };
        return Init(bpmTime, vline.positionX, public_LineDiv);
    }
    public virtual NoteEdit Init(BPMTime bpmTime, float positionX, Public_LineDiv public_LineDiv)
    {
        Note tempNote = thisNote;

        thisNote = new();
        if (isRefresh)
        {
            isRefresh = false;
            thisNote.positionX = tempNote.positionX;
            thisNote.hitTime = tempNote.hitTime;
            thisNote.effect = tempNote.effect;
            thisNote.endTime = tempNote.endTime;
        }
        else
        {
            thisNote.positionX = positionX;
            thisNote.hitTime = new(bpmTime.integer, bpmTime.molecule, bpmTime.denominator);
            thisNote.effect = Blophy.Chart.NoteEffect.Ripple & Blophy.Chart.NoteEffect.CommonEffect;
        }
        HoldLengthHandle();
        float canvasLocalPositionX = (public_LineDiv.vLines.edgeRightVerticalLine.transform.localPosition - public_LineDiv.vLines.edgeLeftVerticalLine.transform.localPosition).x / 2 * thisNote.positionX;
        float canvasLocalPositionY = YScale.Instance.GetPositionYWithSecondsTime(BPMManager.Instance.GetSecondsTimeWithBPMSeconds(thisNote.hitTime.thisStartBPM));
        transform.localPosition = new Vector2(canvasLocalPositionX, canvasLocalPositionY);
        return this;
    }
    public virtual void HoldLengthHandle()
    {
        thisNote.endTime = thisNote.hitTime;
    }
}
