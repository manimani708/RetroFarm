using UnityEngine;
using System.Collections;

public static class ExtensionMethod 
{
    public static GameObject FindGameObject(this GameObject self, string name)
    {
        var result = self.transform.Find(name);
        return result != null ? result.gameObject : null;
    }

    public static GameObject FindGameObject(this Component self, string name)
    {
        var result = self.transform.Find(name);
        return result != null ? result.gameObject : null;
    }

    public static void SetParent(this GameObject self, Component parent)
    {
        self.transform.SetParent(parent.transform);
    }

    public static void SetParent(this GameObject self, GameObject parent)
    {
        self.transform.SetParent(parent.transform);
    }

    public static void SetParent(this Component self, Component parent)
    {
        self.transform.SetParent(parent.transform);
    }

    public static void SetParent(this Component self, GameObject parent)
    {
        self.transform.SetParent(parent.transform);
    }
}

