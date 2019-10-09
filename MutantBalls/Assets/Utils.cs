using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class Utils
{
    // Sets all the MonoBehaviour (even deactivated ones) of a given type to the given state.
    // Only objects within a scene (so no prefabs) are modified.
    public static void SetAllMBEnabled<T>(bool enabled) where T : MonoBehaviour
    {
        var mbs = Resources.FindObjectsOfTypeAll(typeof(T))
            .Cast<MonoBehaviour>()
            .Where(x => !Utils.IsPrefab(x));
        foreach (var mb in mbs)
        {
            mb.enabled = enabled;
        }
    }

    public static bool IsPrefab(Component obj)
    {
        return obj.gameObject.scene.name == null;
    }
}