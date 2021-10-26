using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraExtensions
{
    public static Bounds ExGetBounds(this Camera camera)
    {
        var h = camera.orthographicSize * 2;
        var w = h * camera.aspect;
        var pos = camera.transform.position;
        pos.z = (camera.farClipPlane - camera.nearClipPlane) / 2f + pos.z;
        var cameraBounds = new Bounds(pos, new Vector3(w, h, Mathf.Abs(camera.farClipPlane - camera.nearClipPlane)));
        return cameraBounds;
    }

    public static float ExWorldToScreenDistance(this Camera camera, float worldDistance)
    {
        var ppu = camera.pixelHeight / camera.orthographicSize / 2;
        var screenDistance = worldDistance * ppu;
        return screenDistance;
    }

    public static Vector2 ExWorldToScreenDistance(this Camera camera, Vector2 worldDistance)
    {
        return new Vector2(
            camera.ExWorldToScreenDistance(worldDistance.x),
            camera.ExWorldToScreenDistance(worldDistance.y)
        );
    }

    public static float ExScreenToWorldDistance(this Camera camera, float screenDistance)
    {
        var ppu = camera.pixelHeight / camera.orthographicSize / 2;
        var worldDistance = screenDistance / ppu;
        return worldDistance;
    }

    public static Vector2 ExScreenToWorldDistance(this Camera camera, Vector2 screenDistance)
    {
        return new Vector2(
            camera.ExScreenToWorldDistance(screenDistance.x),
            camera.ExScreenToWorldDistance(screenDistance.y)
        );
    }

    public static Rect ExScreenToWorldRect(this Camera camera, Rect screenRect)
    {
        var pos = camera.ScreenToWorldPoint(screenRect.position);
        var size = camera.ExScreenToWorldDistance(screenRect.size);
        return new Rect(pos, size);
    }
    public static Rect ExWorldToScreenRect(this Camera camera, Rect worldRect)
    {
        var pos = camera.WorldToScreenPoint(worldRect.position);
        var size = camera.ExWorldToScreenDistance(worldRect.size);
        return new Rect(pos, size);
    }
}