using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ComponentsExtensions
{
    public static void SetEnabledAll(this IList<Behaviour> components, bool enabled)
    {
        for (int i = 0; i < components.Count; i++)
        {
            var component = components[i];
            if (component != null)
                component.enabled = enabled;
        }
    }
    public static void SetEnabledAll(this IEnumerable<Behaviour> components, bool enabled)
    {
        foreach (var component in components)
        {
            if (component != null)
                component.enabled = enabled;
        }
    }

    public static void SetActiveAll(this GameObject[] objects, bool activeSelf)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            var gameObject = objects[i];
            if (gameObject != null)
                gameObject.SetActive(activeSelf);
        }
    }

    public static void SetActiveAll(this Component[] objects, bool activeSelf)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            var component = objects[i];
            if (component != null)
                component.gameObject.SetActive(activeSelf);
        }
    }

    public static bool TryGetVisibleBounds(this Component component, out Bounds bounds, Func<Component, bool> predicate = null)
     => TryGetVisibleBounds(component.gameObject, out bounds, predicate);

    public static bool TryGetVisibleBounds(this GameObject objects, out Bounds bounds, Func<Component, bool> predicate = null)
    {
        var hasBounds = false;
        bounds = new Bounds();
        var renderers = objects.GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            var renderer = renderers[i];
            if (renderer == null
                || !renderer.enabled
                || !renderer.isVisible
                || (renderer is SpriteRenderer sr && sr.sprite == null)
                || (predicate != null && !predicate.Invoke(renderer)))
                continue;

            if (!hasBounds)
            {
                hasBounds = true;
                bounds = renderer.bounds;
            }
            else
            {
                bounds.Encapsulate(renderer.bounds);
            }
        }
        return hasBounds;
    }

    public static Bounds GetTransformBounds(this Component component, Func<Component, bool> predicate = null)
     => GetTransformBounds(component.gameObject, predicate);

    public static Bounds GetTransformBounds(this GameObject objects, Func<Component, bool> predicate = null)
    {
        var transforms = objects.GetComponentsInChildren<Transform>(false).AsEnumerable();
        if (predicate != null)
            transforms = transforms.Where(t => predicate.Invoke(t));

        var bounds = transforms.GetBounds();
        bounds.Encapsulate(objects.transform.position);
        return bounds;
    }

    public static Bounds GetLossyBounds(this Component component, Func<Component, bool> predicate = null)
     => GetLossyBounds(component.gameObject, predicate);

    public static Bounds GetLossyBounds(this GameObject objects, Func<Component, bool> predicate = null)
    {
        if (!objects.TryGetVisibleBounds(out var bounds, predicate))
            bounds = GetTransformBounds(objects, predicate);
        return bounds;
    }

    public static Bounds GetBounds(this IEnumerable<Transform> transforms)
    {
        var tr = transforms.Where(t => t != null);

        if (!tr.Any())
            return new Bounds();

        var bounds = new Bounds(tr.First().position, Vector3.zero);
        foreach (var transform in tr.Skip(1))
            bounds.Encapsulate(transform.position);
        return bounds;
    }
}