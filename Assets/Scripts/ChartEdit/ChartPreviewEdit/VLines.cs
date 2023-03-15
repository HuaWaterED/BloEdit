using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VLines : MonoBehaviour
{
    public Transform edgeLeftVerticalLine;
    public Transform edgeRightVerticalLine;
    public List<Transform> middleLines = new();
    public void AddLineOrRemoveLine(int addOrRemove, string thisText)
    {
        for (int i = 0; i < middleLines.Count; i++)
            Destroy(middleLines[i].gameObject);
        middleLines.Clear();
        int middleLinesCount = int.Parse(thisText) + addOrRemove;
        float leftRightDelta = (edgeRightVerticalLine.localPosition - edgeLeftVerticalLine.localPosition).x / (middleLinesCount + 1);
        for (int i = 1; i < middleLinesCount + 1; i++)
        {
            Transform instLine = Instantiate(edgeLeftVerticalLine, transform);
            instLine.localPosition = new Vector3(edgeLeftVerticalLine.transform.localPosition.x + leftRightDelta * i, edgeLeftVerticalLine.transform.localPosition.y);
            middleLines.Add(instLine);
        }
    }
}
