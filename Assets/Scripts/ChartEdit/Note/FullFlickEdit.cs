using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullFlickEdit : NoteEdit
{
    public override NoteEdit Init(BeatLine beatLine, VLine vline, Public_LineDiv public_LineDiv)
    {
        base.Init(beatLine, vline, public_LineDiv);
        thisNote.noteType = Blophy.Chart.NoteType.FullFlickBlue;
        EventsEdit_Edit.Instance.UpdateEditingInfo(this, true);
        return this;
    }
}
