using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventVLine : VLine
{
    public EventType eventType;
    public List<EventEdit> eventsEditList;
    public void AddEventEdit2ChartDataEvent()
    {
        Blophy.Chart.BoxEvents boxEvents = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents;
        List<Blophy.Chart.Event> events = eventType switch
        {
            EventType.centerX => boxEvents.centerX,
            EventType.centerY => boxEvents.centerY,
            EventType.moveX => boxEvents.moveX,
            EventType.moveY => boxEvents.moveY,
            EventType.scaleX => boxEvents.scaleX,
            EventType.scaleY => boxEvents.scaleY,
            EventType.rotate => boxEvents.rotate,
            EventType.alpha => boxEvents.alpha,
            EventType.lineAlpha => boxEvents.lineAlpha,
            EventType.speed => null,
            _ => throw new System.Exception("Ohhhhh...没找到对应的事件类型")
        };
        events ??= Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[0].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[1].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[2].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[3].Speed
            = Chart.Instance.chartData.boxes[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[4].Speed;
        Algorithm.BubbleSort(eventsEditList, (a, b) =>
        {
            if (a.thisEvent.startTime.thisStartBPM > b.thisEvent.startTime.thisStartBPM)
            {
                return 1;
            }
            else if (a.thisEvent.startTime.thisStartBPM < b.thisEvent.startTime.thisStartBPM)
            {
                return -1;
            }
            return 0;
        });

        switch (eventType)
        {
            case EventType.speed:
                List<Blophy.ChartEdit.Event> allSpeed
                    = Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[0].speed
                      = Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[1].speed
                      = Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[2].speed
                      = Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[3].speed
                      = Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].lines[4].speed;
                allSpeed.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    allSpeed.Add(eventsEditList[i].thisEvent);
                }
                break;
            case EventType.centerX:
                Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.centerX.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.centerX.Add(eventsEditList[i].thisEvent);
                }
                break;
            case EventType.centerY:
                Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.centerY.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.centerY.Add(eventsEditList[i].thisEvent);
                }
                break;
            case EventType.moveX:
                Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.moveX.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.moveX.Add(eventsEditList[i].thisEvent);
                }
                break;
            case EventType.moveY:
                Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.moveY.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.moveY.Add(eventsEditList[i].thisEvent);
                }
                break;
            case EventType.scaleX:
                Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.scaleX.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.scaleX.Add(eventsEditList[i].thisEvent);
                }
                break;
            case EventType.scaleY:
                Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.scaleY.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.scaleY.Add(eventsEditList[i].thisEvent);
                }
                break;
            case EventType.rotate:
                Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.rotate.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.rotate.Add(eventsEditList[i].thisEvent);
                }
                break;
            case EventType.alpha:
                Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.alpha.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.alpha.Add(eventsEditList[i].thisEvent);
                }
                break;
            case EventType.lineAlpha:
                Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.lineAlpha.Clear();
                for (int i = 0; i < eventsEditList.Count; i++)
                {
                    Chart.Instance.boxesEdit[int.Parse(BoxNumber.Instance.thisText.text) - 1].boxEvents.lineAlpha.Add(eventsEditList[i].thisEvent);
                }
                break;
        }

        events.Clear();
        for (int i = 0; i < eventsEditList.Count; i++)
        {
            Blophy.ChartEdit.Event currentEventEdit = eventsEditList[i].thisEvent;
            Blophy.Chart.Event chartDataEvent = new();
            chartDataEvent.startTime = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(currentEventEdit.startTime.thisStartBPM);
            chartDataEvent.endTime = BPMManager.Instance.GetSecondsTimeWithBPMSeconds(currentEventEdit.endTime.thisStartBPM);
            chartDataEvent.startValue = currentEventEdit.startValue;
            chartDataEvent.endValue = currentEventEdit.endValue;
            chartDataEvent.curve = currentEventEdit.curve;
            events.Add(chartDataEvent);
        }
    }
}