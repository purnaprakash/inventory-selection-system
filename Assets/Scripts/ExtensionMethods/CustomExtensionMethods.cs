using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomExtensionMethods
{
    public static void SetAnchorPresetsToStretchAll(this RectTransform rt)
    {
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.anchoredPosition = Vector2.zero;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }
}
