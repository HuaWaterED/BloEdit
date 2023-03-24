using Blophy.ChartEdit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blophy.Chart
{
    public class ChartTools
    {
        public static void EditEvent2ChartDataEvent(List<Blophy.Chart.Event> chartEvents, List<Blophy.ChartEdit.Event> editEvents)
        {

        }
        public static void EditNote2ChartDataNote(Line chartNotes, List<Blophy.ChartEdit.Note> editNotes)
        {
            chartNotes.onlineNotes.Clear();
            for (int i = 0; i < editNotes.Count; i++)
            {
                ChartEdit.Note editNote = editNotes[i];
                Blophy.Chart.Note note = new();
                note.hitTime = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(editNote.hitTime.thisStartBPM);
                note.isClockwise = editNote.isClockwise;
                note.noteType = editNote.noteType;
                //note.positionX = editNote.positionX * .75f;
                note.positionX = note.noteType switch
                {
                    NoteType.FullFlickPink => editNote.positionX,
                    NoteType.FullFlickBlue => editNote.positionX,
                    _ => editNote.positionX * .75f,
                };
                note.effect = editNote.effect;
                note.holdTime = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(editNote.endTime.thisStartBPM)
                    - BPMManager.Instance.GetSecondsTimeWithBPMSeconds(editNote.hitTime.thisStartBPM);
                //note.hasOther
                //(float)Math.Round(canvasLocalOffset.Evaluate(notes[i].hitTime), 3);
                note.hitFloorPosition = (float)Math.Round(chartNotes.far.Evaluate(note.hitTime), 3);
                chartNotes.onlineNotes.Add(note);
            }
        }
        public static ChartData CreateNew()
        {
            ChartData data = new();
            data.globalData = new();
            data.globalData.BPMlist = new();
            data.globalData.BPMlist.Add(new() { integer = 0, molecule = 0, denominator = 1, currentBPM = 60 });
            data.globalData.musicLength = AssetManager.Instance.musicPlayer.clip.samples / (float)AssetManager.Instance.musicPlayer.clip.frequency;
            data.metaData = new();
            data.boxes = new();
            data.boxes.Add(new());
            data.boxes[0].boxEvents = new();
            data.boxes[0].lines = new();
            data.boxes[0].lines.Add(new());
            data.boxes[0].lines.Add(new());
            data.boxes[0].lines.Add(new());
            data.boxes[0].lines.Add(new());
            data.boxes[0].lines.Add(new());
            for (int i = 0; i < data.boxes[0].lines.Count; i++)
            {
                Public_AnimationCurveEaseEnum.keyValuePairs.TryGetValue(0, out AnimationCurve linear);
                data.boxes[0].lines[i].Speed = new();
                data.boxes[0].lines[i].Speed.Add(new()
                {
                    startTime = 0,
                    endTime = 200,
                    startValue = 2,
                    endValue = 2,
                    curve = linear
                });
                SpeedEvents2Far(data.boxes[0].lines[i]);
                data.boxes[0].lines[i].offlineNotes = new();
                data.boxes[0].lines[i].onlineNotes = new();
            }
            data.boxes[0].boxEvents.scaleX = new();
            data.boxes[0].boxEvents.scaleY = new();
            data.boxes[0].boxEvents.moveX = new();
            data.boxes[0].boxEvents.moveY = new();
            data.boxes[0].boxEvents.centerX = new();
            data.boxes[0].boxEvents.centerY = new();
            data.boxes[0].boxEvents.alpha = new();
            data.boxes[0].boxEvents.lineAlpha = new();
            data.boxes[0].boxEvents.rotate = new();


            return data;
        }

        public static void SpeedEvents2Far(Line line)
        {
            List<Keyframe> keyframes = GameUtility.CalculatedSpeedCurve(line.Speed.ToArray());//将获得到的Key列表全部赋值
            AnimationCurve canvasSpeed = new() { keys = keyframes.ToArray(), preWrapMode = WrapMode.ClampForever, postWrapMode = WrapMode.ClampForever };//把上边获得到的点转换为速度图
            line.career = canvasSpeed;
            line.far = GameUtility.CalculatedOffsetCurve(canvasSpeed, keyframes);//吧速度图转换为位移图
        }
    }

    [Serializable]
    //public struct ChartData
    public class ChartData
    {
        public MetaData metaData;
        public List<Box> boxes;
        public GlobalData globalData;
        public List<Text> texts;
    }

    [Serializable]
    public class MetaData
    {
        public string musicName = "";
        public string musicWriter = "";
        public string musicBPMText = "";
        public string artWriter = "";
        public string chartWriter = "";
        public string chartLevel = "";
        public string description = "";
    }
    [Serializable]
    //public struct GlobalData
    public class GlobalData
    {
        public float offset = 0;
        public float musicLength = 0;
        public List<BPM> BPMlist;
        public int tapCount = 0;
        public int holdCount = 0;
        public int dragCount = 0;
        public int flickCount = 0;
        public int fullFlickCount = 0;
        public int pointCount = 0;
    }
    [Serializable]
    public class Text
    {
        //X Y 间距 文字大小 颜色 旋转 透明度
        public float startTime;
        public float endTime;
        public float size;
        public string text;
        public Event moveX;
        public Event moveY;

        public List<EventString> thisEvent;
        public List<Event> positionX;
        public List<Event> positionY;
        public List<Event> spaceBetween;
        public List<Event> textSize;
        public List<Event> r;
        public List<Event> g;
        public List<Event> b;
        public List<Event> rotate;
        public List<Event> alpha;

    }
    #region 下面都是依赖
    [Serializable]
    public class BPM
    {
        public int integer = 0;
        public int molecule = 0;
        public int denominator = 1;
        public float thisStartBPM => integer + molecule / (float)denominator;
        public float currentBPM;
        public void AddOneBeat()
        {
            denominator = int.Parse(HorizontalLine.Instance.thisText.text) + 1;
            if (molecule < denominator - 1) molecule++;
            else if (molecule + 1 >= denominator)
            {
                molecule = 0;
                integer++;
            }
        }
        public BPM() { }
        public BPM(BPM bpm)
        {
            this.molecule = bpm.molecule;
            this.denominator = bpm.denominator;
            this.integer = bpm.integer;
            this.currentBPM = bpm.currentBPM;
        }
    }
    [Serializable]
    //public struct Box
    public class Box
    {
        public BoxEvents boxEvents;
        public List<Line> lines;
    }
    [Serializable]
    //public struct Line
    public class Line
    {
        public List<Note> onlineNotes;
        public List<Note> offlineNotes;
        [SerializeField] List<Event> speed;
        public List<Event> Speed
        {
            get => speed;
            set
            {
                speed = value;
                List<Keyframe> keyframes = GameUtility.CalculatedSpeedCurve(value.ToArray());//将获得到的Key列表全部赋值
                AnimationCurve canvasSpeed = new() { keys = keyframes.ToArray(), preWrapMode = WrapMode.ClampForever, postWrapMode = WrapMode.ClampForever };//把上边获得到的点转换为速度图
                career = canvasSpeed;
                far = GameUtility.CalculatedOffsetCurve(canvasSpeed, keyframes);//吧速度图转换为位移图
            }
        }
        public AnimationCurve far;//画布偏移绝对位置，距离
        public AnimationCurve career;//速度
        //public static void SpeedEvents2Far(Line line)
        //{
        //    List<Keyframe> keyframes = GameUtility.CalculatedSpeedCurve(line.Speed.ToArray());//将获得到的Key列表全部赋值
        //    AnimationCurve canvasSpeed = new() { keys = keyframes.ToArray(), preWrapMode = WrapMode.ClampForever, postWrapMode = WrapMode.ClampForever };//把上边获得到的点转换为速度图
        //    line.career = canvasSpeed;
        //    line.far = GameUtility.CalculatedOffsetCurve(canvasSpeed, keyframes);//吧速度图转换为位移图
        //}
    }
    [Serializable]
    //public struct Note
    public class Note
    {
        public NoteType noteType;
        public NoteEffect effect;
        public float hitTime;//打击时间
        public float positionX;
        public bool isClockwise;//是逆时针
        public float holdTime;
        public bool hasOther;//还有别的Note和他在统一时间被打击，简称多押标识（（
        [JsonIgnore]
        public float HoldTime
        {
            get => holdTime == 0 ? JudgeManager.bad : holdTime;
            set => holdTime = value;
        }
        [JsonIgnore] public float EndTime => hitTime + HoldTime;
        [JsonIgnore] public float hitFloorPosition;//打击地板上距离
    }
    [Serializable]
    public enum NoteType
    {
        Tap = 0,
        Hold = 1,
        Drag = 2,
        Flick = 3,
        Point = 4,
        FullFlickPink = 5,
        FullFlickBlue = 6
    }
    [Flags]
    [Serializable]
    public enum NoteEffect
    {
        Ripple = 1,
        FullLine = 2,
        CommonEffect = 4
    }
    [Serializable]
    //public struct BoxEvents
    public class BoxEvents
    {
        public List<Event> moveX;
        public List<Event> moveY;
        public List<Event> rotate;
        public List<Event> alpha;
        public List<Event> scaleX;
        public List<Event> scaleY;
        public List<Event> centerX;
        public List<Event> centerY;
        public List<Event> lineAlpha;
    }
    [Serializable]
    //public struct Event
    public class Event
    {
        public float startTime;
        public float endTime;
        public float startValue;
        public float endValue;
        public AnimationCurve curve;
    }
    [Serializable]
    public class EventString
    {
        public float startTime;
        public float endTime;
        public string startValue;
        public string endValue;
    }
    #endregion
}
namespace Blophy.ChartEdit
{
    [Serializable]
    //public struct Box
    public class Box
    {
        public BoxEvents boxEvents;
        public List<Line> lines;
    }
    [Serializable]
    public class Text
    {

    }
    [Serializable]
    //public struct BoxEvents
    public class BoxEvents
    {
        public List<Event> moveX;
        public List<Event> moveY;
        public List<Event> rotate;
        public List<Event> alpha;
        public List<Event> scaleX;
        public List<Event> scaleY;
        public List<Event> centerX;
        public List<Event> centerY;
        public List<Event> lineAlpha;
    }
    [Serializable]
    //public struct Line
    public class Line
    {
        public List<Note> onlineNotes;
        public List<Note> offlineNotes;
        public List<Event> speed;
    }
    [Serializable]
    //public struct Note
    public class Note
    {
        public Blophy.Chart.NoteType noteType;
        public Blophy.Chart.NoteEffect effect;
        public BPMTime hitTime;//打击时间
        public float positionX;
        public bool isClockwise;//是逆时针
        public BPMTime endTime;
        public bool hasOther;//还有别的Note和他在统一时间被打击，简称多押标识（（
    }
    [Serializable]
    //public struct Event
    public class Event
    {
        public BPMTime startTime;
        public BPMTime endTime;
        public float startValue;
        public float endValue;
        public AnimationCurve curve;
    }
    //[Serializable]
    //public enum NoteType
    //{
    //    Tap = 0,
    //    Hold = 1,
    //    Drag = 2,
    //    Flick = 3,
    //    Point = 4,
    //    FullFlickPink = 5,
    //    FullFlickBlue = 6
    //}
    //[Flags]
    //[Serializable]
    //public enum NoteEffect
    //{
    //    Ripple = 1,
    //    FullLine = 2,
    //    CommonEffect = 4
    //}
    [Serializable]
    public class BPMTime
    {
        public int integer = 0;
        public int molecule = 0;
        public int denominator = 1;
        public float thisStartBPM => integer + molecule / (float)denominator;
        public BPMTime() { }
        public BPMTime(int integer, int molecule, int denominator)
        {
            this.integer = integer;
            this.molecule = molecule;
            this.denominator = denominator;
        }
        public BPMTime(BPMTime bpmTime)
        {
            integer = bpmTime.integer;
            molecule = bpmTime.molecule;
            denominator = bpmTime.denominator;
        }
        public static BPMTime Zero => new();
    }
}