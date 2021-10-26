using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static T GetTopmostComponentInParent<T>(this GameObject go, bool includeThisGameObject = true) where T : Component
    {
        var component = go.GetComponentInParent<T>();

        var parentComp = component;
        while (parentComp != null && component.transform.parent != null)
        {
            parentComp = component.transform.parent.GetComponentInParent<T>();
            if (parentComp != null)
                component = parentComp;
        }

        if (!includeThisGameObject && component != null && component.gameObject == go)
            component = null;
        return component;
    }

    public static T GetTopmostComponentInParent<T>(this Component component) where T : Component
        => component.gameObject.GetTopmostComponentInParent<T>();

    /// <summary>
    /// Breadth-first search
    /// </summary>
    public static Transform FindDeepChildB(this Transform parent, string name)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(parent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == name)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }

    /// <summary>
    /// Depth-first search
    /// </summary>
    public static Transform FindDeepChildD(this Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;
            var result = child.FindDeepChildD(name);
            if (result != null)
                return result;
        }
        return null;
    }

    public static void SetLossyScale(this Transform transform, Vector3 lossyScale)
    {
        var currnetLossy = transform.lossyScale;
        var currentLocal = transform.localScale;

        transform.localScale = lossyScale
            .ExScale(currentLocal)
            .InverseScale(currnetLossy);
    }

    public static void DetroyAllChildrens(this Transform parent)
    {
        foreach (Transform child in parent)
            GameObject.Destroy(child.gameObject);
    }

    public static void DetroyAllChildrensImmediate(this Transform parent, bool allowDestroyInAssets = false)
    {
        var childCount = parent.childCount;
        while(childCount > 0)
        {
            childCount--;
            GameObject.DestroyImmediate(parent.GetChild(0).gameObject, allowDestroyInAssets);
        }
    }
}