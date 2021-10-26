using UnityEngine;

public static class BoundsExtensions
{
    public static Bounds WithCenter(this Bounds bounds, Vector3 center)
    {
        bounds.center = center;
        return bounds;
    }
    public static Bounds WithCenterAdd(this Bounds bounds, Vector3 centerAdd)
    {
        bounds.center += centerAdd;
        return bounds;
    }

    public static Bounds WithSize(this Bounds bounds, Vector3 size)
    {
        bounds.size = size;
        return bounds;
    }
    public static Bounds WithSizeScale(this Bounds bounds, Vector3 sizeScale)
    {
        bounds.size = bounds.size.ExScale(sizeScale);
        return bounds;
    }

    public static Rect To2dRect(this Bounds bouns) => new Rect(bouns.min, bouns.size);
}