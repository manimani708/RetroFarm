using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;

public class GameManager : SingletonMonoBehaviour<GameManager> 
{
    [NonSerializedAttribute]
    public List<NutsData> nutsDic = new List<NutsData>();


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        double seconds = GetTimeCountFromQuit();
        Debug.Log(seconds);

        // RELEASE シンボルが定義されている場合はデバッグメニューのシーンを読み込まない
#if RELEASE
#else
        SceneManager.LoadScene("DebugMenu", LoadSceneMode.Additive);
#endif

        Load();

    }

    void OnDestroy()
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteKey("StartUpTime");
            Debug.Log("Delete");
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("Test");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }

       
    }

    void OnApplicationQuit()
    {
        Save();
    }

    //アプリケーションを起動した時に、前回終了時からの経過時間を取得する
    private double GetTimeCountFromQuit()
    {
        System.DateTime datetime;

        System.DateTime now = System.DateTime.Now;
        Debug.Log(now);

        string datetimeString = PlayerPrefs.GetString("StartUpTime");
        //前回終了時刻を取得
        if (datetimeString != "")
        {
            datetime = System.DateTime.FromBinary(System.Convert.ToInt64(datetimeString));
        }
        else
        {
            //初回起動時は起動時刻を取得
            datetime = now;
            PlayerPrefs.SetString("StartUpTime", now.ToBinary().ToString());
        }
        Debug.Log(datetime);

        return (now - datetime).TotalSeconds;
    }



    private void Save()
    {
        //リストが空でなければリストの全要素を削除
        if(nutsDic != null)
        {
            nutsDic.Clear(); 
        }
        else
        {
            //空だったら領域確保
            nutsDic = new List<NutsData>();
        }

        //シーン上にある木の実のデータを取得してリストに追加
        GameObject[] nuts = GameObject.FindGameObjectsWithTag("Nuts");
        foreach(GameObject obj in nuts)
        {
            Nuts tmp = obj.GetComponent<Nuts>();
            nutsDic.Add(new NutsData((int)tmp.state, tmp.elapsedTimeInState, tmp.parentInstanceID));
        }

        //JSONファイルにセーブ
        JsonManager.Save(new Serialization<NutsData>(nutsDic), "SampleData.json");
    }



    private void Load()
    {
        //JSONファイルからデータをロード
        nutsDic = JsonManager.Load("SampleData.json") as List<NutsData>;

        //ロードしたデータに基づいて木の実を生成
        FarmManager.Instance.CreateNutsOnStart();
    }
}




