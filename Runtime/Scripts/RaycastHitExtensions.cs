using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastHitExtensions 
{
    public static bool IsInLaerMask (this RaycastHit2D hit2D, LayerMask mask)
        => hit2D && ((mask & 1 << hit2D.transform.gameObject.layer) != 0);
}
