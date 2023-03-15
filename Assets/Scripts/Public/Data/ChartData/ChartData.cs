using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Blophy.Chart
{

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
        public float offset;
        public float musicLength;
        public List<BPM> BPMlist;
        public int tapCount;
        public int holdCount;
        public int dragCount;
        public int flickCount;
        public int fullFlickCount;
        public int pointCount;
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
        int onlineNotesLength = -1;
        public int OnlineNotesLength
        {
            get
            {
                if (onlineNotesLength < 0) onlineNotesLength = onlineNotes.Count;
                return onlineNotesLength;
            }
        }
        public List<Note> offlineNotes;
        int offlineNotesLength = -1;
        public int OfflineNotesLength
        {
            get
            {
                if (offlineNotesLength < 0) offlineNotesLength = offlineNotes.Count;
                return offlineNotesLength;
            }
        }
        public List<Event> speed;
        public AnimationCurve far;//画布偏移绝对位置，距离
        public AnimationCurve career;//速度
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
        public AnimationCurve far;//画布偏移绝对位置，距离
        public AnimationCurve career;//速度
    }
    [Serializable]
    //public struct Note
    public class Note
    {
        public NoteType noteType;
        public NoteEffect effect;
        public BPMTime hitTime;//打击时间
        public float positionX;
        public bool isClockwise;//是逆时针
        public BPMTime holdTime;
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
    public class BPMTime
    {
        public int integer = 0;
        public int molecule = 0;
        public int denominator = 1;
    }
}