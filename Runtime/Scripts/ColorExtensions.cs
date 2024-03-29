using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtensions
{
    public static Color WithR(this Color color, float r)
    {
        color.r = r;
        return color;
    }
    public static Color WithG(this Color color, float g)
    {
        color.g = g;
        return color;
    }
    public static Color WithB(this Color color, float b)
    {
        color.b = b;
        return color;
    }
    public static Color WithA(this Color color, float a)
    {
        color.a = a;
        return color;
    }

    public static string ToHexString(this Color color) => ColorUtility.ToHtmlStringRGBA(color);
}