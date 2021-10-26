using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RectTransformExtensions
{
    public static RectTransformData CopyData(this RectTransform targetTransform)
    {
        var data = new RectTransformData();
        data.pivot = targetTransform.pivot;
        data.position = targetTransform.position;
        data.sizeDelta = targetTransform.sizeDelta;
        data.lossyScale = targetTransform.lossyScale;
        data.rotation = targetTransform.rotation;
        return data;
    }

    public static void CopyPasteFrom(this RectTransform targetTransform, RectTransform sourceTransform)
    {
        if (sourceTransform == null)
            return;

        var data = sourceTransform.CopyData();
        data.PasteTo(targetTransform);
    }

    public static Rect WithPosition(this Rect rect, Vector2 position)
    {
        rect.position = position;
        return rect;
    }

    public static Rect Encapsulate(this Rect rect, Rect otherRect)
    {
        var rectBounds = new Bounds(rect.center, rect.size);
        rectBounds.Encapsulate(new Bounds(otherRect.center, otherRect.size));
        return new Rect(rectBounds.min, rectBounds.size);
    }

    private static Vector3[] _corners = new Vector3[4];
    public static Rect GetWorldRect(this RectTransform rt)
    {
        // var size = Vector2.Scale(rt.rect.size, rt.lossyScale);
        // var rect = new Rect(
        //     (Vector2)rt.position - (size * 0.5f),
        //     size
        // );
        // return rect;

        rt.GetWorldCorners(_corners);

        var min = new Vector2(
            _corners.Min(c => c.x),
            _corners.Min(c => c.y)
        );
        var max = new Vector2(
            _corners.Max(c => c.x),
            _corners.Max(c => c.y)
        );
        return new Rect(min, max - min);
    }

    public static Rect GetWorldRect(this IEnumerable<RectTransform> transforms)
    {
        if (transforms == null)
            throw new System.ArgumentNullException(nameof(transforms));

        transforms = transforms.Where(t => t != null);

        if (!transforms.Any())
            return Rect.zero;

        var rect = transforms.First().GetWorldRect();
        foreach (var rectTransform in transforms.Skip(1))
            rect = rect.Encapsulate(rectTransform.GetWorldRect());

        return rect;
    }

    public static float GetArea(this Vector2 size)
    {
        size.x = Mathf.Abs(size.x);
        size.y = Mathf.Abs(size.y);
        return size.x * size.y;
    }
    public static float GetArea(this Rect rect) => rect.size.GetArea();

    public static bool TryGetIntersections(this Rect rect, Rect otherRect, bool includeZeroSize, out Rect intersection)
        => Utils.Math.TryGetIntersection(rect, otherRect, includeZeroSize, out intersection);

    public static bool IsInside(this Rect rect, Rect outterRect)
        => Utils.Math.IsInside(outterRect, rect);
}

public struct RectTransformData
{
    public Vector2 pivot;
    public Vector3 position;
    public Vector2 sizeDelta;
    public Vector3 lossyScale;
    public Quaternion rotation;

    public void PasteTo(RectTransform targetTransform)
    {
        if (targetTransform == null)
            return;
        targetTransform.pivot = pivot;
        targetTransform.position = position;
        targetTransform.sizeDelta = sizeDelta;
        targetTransform.SetLossyScale(lossyScale);
        targetTransform.rotation = rotation;
    }
}