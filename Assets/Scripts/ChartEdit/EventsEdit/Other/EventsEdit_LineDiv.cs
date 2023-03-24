using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsEdit_LineDiv : Public_LineDiv
{
    public override void RefreshNoteEdits()
    {
        ClearAllEvents();
        List<VLines> eventVLines = VerticalLine.Instance.eventVLines;
        int currentBoxNumber = int.Parse(BoxNumber.Instance.thisText.text);
        //var thisBoxEvents=
        /*
         * List<Blophy.ChartEdit.Note> notesOnThisCanvas = Chart.Instance.boxesEdit[exeBoxNumber].lines[exeLineNumber].onlineNotes;
        for (int i = 0; i < notesOnThisCanvas.Count; i++)
        {
            NoteEdit note = notesOnThisCanvas[i].noteType switch
            {
                NoteType.Tap => AddTap.Instance.thisNote,
                NoteType.Hold => AddHold.Instance.thisNote,
                NoteType.Drag => AddDrag.Instance.thisNote,
                NoteType.Flick => AddFlick.Instance.thisNote,
                NoteType.Point => AddPoint.Instance.thisNote,
                NoteType.FullFlickPink => AddFullFlick.Instance.thisNote,
                NoteType.FullFlickBlue => AddFullFlick.Instance.thisNote,
                _ => throw new System.Exception("哼哼啊啊啊啊啊啊啊啊啊1145141919810，你知道为什么报错嘛？哼哼啊啊啊啊啊啊啊啊啊，报错的原因是，没有找到音符类型哼哼啊啊啊啊啊啊啊啊啊")
            };
            note.thisNote = notesOnThisCanvas[i];
            NoteEdit instNote = Instantiate(note, Vector2.zero, Quaternion.identity, notesCanvas.transform).IsRefresh().Init(note.thisNote.hitTime, note.thisNote.positionX, this);
            noteEdits.Add(instNote);
            ChartTools.EditNote2ChartDataNote(
            Chart.Instance.chartData.boxes[exeBoxNumber].lines[exeLineNumber],
            Chart.Instance.boxesEdit[exeBoxNumber].lines[exeLineNumber].onlineNotes);
        }
         */
    }

    private static void ClearAllEvents()
    {
        List<VLines> eventVLines = VerticalLine.Instance.eventVLines;
        for (int i = 0; i < eventVLines.Count; i++)
        {
            EventVLine edgeLeftVerticalLine = (EventVLine)eventVLines[i].edgeLeftVerticalLine;
            EventVLine edgeRightVerticalLine = (EventVLine)eventVLines[i].edgeRightVerticalLine;

            for (int j = 0; j < edgeLeftVerticalLine.eventsEditList.Count; j++)
            {
                EventEdit eventEdit = edgeLeftVerticalLine.eventsEditList[i];
                Destroy(eventEdit.gameObject);
            }
            edgeLeftVerticalLine.eventsEditList.Clear();

            for (int j = 0; j < edgeRightVerticalLine.eventsEditList.Count; j++)
            {
                EventEdit eventEdit = edgeRightVerticalLine.eventsEditList[i];
                Destroy(eventEdit.gameObject);
            }
            edgeLeftVerticalLine.eventsEditList.Clear();

            for (int j = 0; j < eventVLines[i].middleLines.Count; j++)
            {
                EventVLine eventVLine = (EventVLine)eventVLines[i].middleLines[j];
                for (int k = 0; k < edgeRightVerticalLine.eventsEditList.Count; k++)
                {
                    EventEdit eventEdit = eventVLine.eventsEditList[i];
                    Destroy(eventEdit.gameObject);
                }
                eventVLine.eventsEditList.Clear();
            }
        }
    }
}
