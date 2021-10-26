using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Extensions
{
    public static float Abs(this float number) => Mathf.Abs(number);
    public static float AvgXY(this Vector3 vector) => (vector.x + vector.y) / 2f;
    public static float Avg(this Vector3 vector) => (vector.x + vector.y + vector.z) / 3f;
}
