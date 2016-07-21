using System;
using UnityEngine;
using System.Collections;

public class DebugMenu : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
       
    }


    void OnGUI()
    {
        GUILayout.Label(Application.unityVersion);
        GUILayout.Label(Time.timeScale.ToString());
        GUILayout.Label(Time.time.ToString());
        var json = JsonUtility.ToJson(new Serialization<NutsData>(GameManager.Instance.nutsDic),true);
        GUILayout.Label(json);

    }

}


