using UnityEngine;
using System.Collections;
using System;

public class FarmManager : SingletonMonoBehaviour<FarmManager> 
{
    [SerializeField]
    private GameObject nutsPrefab;


	void Update () 
    {
        //タップした位置のオブジェクトのアクションを実行
        if (Input.GetMouseButtonDown(0))
        {

            Vector3 aTapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D aCollider2d = Physics2D.OverlapPoint(aTapPoint);

            if (aCollider2d)
            {
                GameObject obj = aCollider2d.transform.gameObject;
                if(obj.tag == "Soil")
                {
                    //子要素Nutsを取得→無かったら生成、あったら削除
                    GameObject nuts = obj.FindGameObject("Nuts(Clone)");
                    if (nuts == null)
                    {
                        nuts = (GameObject)Instantiate(nutsPrefab,obj.transform.position,nutsPrefab.transform.rotation);
                        nuts.SetParent(obj);
                        nuts.GetComponent<Nuts>().parentInstanceID = obj.GetInstanceID();
                    }
                    else
                    {
                        Destroy(nuts);
                    }
                }
            }
        }
	}


    //前回終了時に存在した木の実をゲーム開始時に生成
    public void CreateNutsOnStart()
    {
        if (GameManager.Instance.nutsDic != null)
        {
            foreach(NutsData nutsData in GameManager.instance.nutsDic)
            {
              if (nutsData != null)
                {
                    GameObject go = Instantiate(nutsPrefab);
                    Nuts comp = go.GetComponent<Nuts>();
                    comp.Init(nutsData.GetState(), nutsData.GetElapsedTimeInState(),nutsData.GetparentInstanceID());
                }  
            }


        }
    }



}
