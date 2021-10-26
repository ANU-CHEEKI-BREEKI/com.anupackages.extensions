using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapExtensions
{
    public static bool ForeachTile(this Tilemap tileMap, bool includeNull, Func<TileBase, Vector3Int, bool> predicate)
    {
        var bounds = tileMap.cellBounds;
        foreach (var position in bounds.allPositionsWithin)
        {
            var tile = tileMap.GetTile(position);

            if (!includeNull && tile == null)
                continue;

            if (!predicate.Invoke(tile, position))
                return false;
        }
        return true;
    }
}