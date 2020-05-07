using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CustomMath
{
    public struct Range
    {
        public float min;
        public float max;
        public float range {get {return max-min + 1;}}
        public Range(float aMin, float aMax)
        {
            min = aMin; 
            max = aMax;
        }
    }
    
    [Serializable]
    public struct Borders
    {
        public float left;
        public float top;
        public float right;
        public float bottom;

        public Vector3[] GetPoints(Vector3 offset = new Vector3())
        {
            return new[]
            {
                new Vector3(-left, 0f, top) + offset,
                new Vector3(right, 0f, top) + offset,
                new Vector3(right, 0f, -bottom) + offset,
                new Vector3(-left, 0f, -bottom) + offset
            };
        }

        public void Normalize(Borders externalBorders)
        {
            left = Mathf.Clamp(left, -externalBorders.left, externalBorders.left);
            top = Mathf.Clamp(top, -externalBorders.top, externalBorders.top);
            right = Mathf.Clamp(right, -externalBorders.right, externalBorders.right);
            bottom = Mathf.Clamp(bottom, -externalBorders.bottom, externalBorders.bottom);
        }
    }
 
    public static float RandomValueFromRanges(params Range[] ranges)
    {
        if (ranges.Length == 0)
            return 0;
        float count = 0;
        foreach (Range r in ranges)
            count += r.range;
        float sel = UnityEngine.Random.Range(0, count);
        foreach (Range r in ranges)
        {
            if (sel < r.range)
            {
                return r.min + sel;
            }
            sel -= r.range;
        }
        throw new Exception("This should never happen");
    }
}
