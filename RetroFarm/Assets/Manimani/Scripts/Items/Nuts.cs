using UnityEngine;
using System.Collections;
using System;


public class Nuts : MonoBehaviour
{
    public enum State
    {
        Seed,               //植えたばかりの状態
        Bud,                //芽が出た状態
        GrouthNotNuts,      //木が成長したけど木の実はできていない状態
        GrouthWithNuts      //木が成長して木の実もできた状態（収穫可）
    }

    [SerializeField]
    private float takeHoursUntilAging;   //熟成までにかかる時間(単位：時間)


    public State state { get; private set; }                //木の実の状態
    public float elapsedTimeInState { get; private set;}    //現在の状態における経過時間
    public int parentInstanceID { get; set;}                //親オブジェクトのインスタンスID

    void Awake()
    {
        state = State.Seed;


    }

    void Update()
    {
        Grow();
    }



    private void Grow()
    {
        Color nextColor = new Color();

        //経過時間の更新
        elapsedTimeInState += Time.deltaTime;
       // Debug.Log(elapsedTimeInState);

        switch(state)
        {
            case State.Seed:
                nextColor = Color.yellow;
                break;
            case State.Bud:
                nextColor = Color.green;
                break;

            case State.GrouthNotNuts:
                nextColor = Color.red;
                break;
            case State.GrouthWithNuts:
                
                break;
        }

        //指定時間経ったら次の状態に成長する
        if (elapsedTimeInState >= takeHoursUntilAging / 3f && state != State.GrouthWithNuts)
        {
            state++;
            elapsedTimeInState = 0f;
            GetComponent<SpriteRenderer>().color = nextColor;
        }

    }


    public void Init(int state, float elapsedTimeInState, int parentInstanceID)
    {
        this.state = (State)Enum.ToObject(typeof(State), state);
        switch (this.state)
        {
            case State.Seed:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case State.Bud:
                GetComponent<SpriteRenderer>().color = Color.yellow;
                break;

            case State.GrouthNotNuts:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
            case State.GrouthWithNuts:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }
        this.elapsedTimeInState = elapsedTimeInState;
        this.parentInstanceID = parentInstanceID;
        GameObject[] soils = GameObject.FindGameObjectsWithTag("Soil");
        foreach(GameObject obj in soils)
        {
            if(obj.GetInstanceID() == parentInstanceID)
            {
                this.SetParent(obj);
                this.transform.position = obj.transform.position;
            }
        }

    }

}


//JSONファイル記述用データクラス
[Serializable]
public class NutsData
{
    [SerializeField]
    private int state;

    [SerializeField]
    private float elapsedTimeInState;

    [SerializeField]
    private int parentInstanceID;


    public NutsData(int state, float elapsedTimeInState, int parentInstanceID)
    {
        this.state = state;
        this.elapsedTimeInState = elapsedTimeInState;
        this.parentInstanceID = parentInstanceID;
    }

    public int GetState()
    {
        return state;
    }

    public float GetElapsedTimeInState()
    {
        return elapsedTimeInState;
    }

    public int GetparentInstanceID()
    {
        return parentInstanceID;
    }

}