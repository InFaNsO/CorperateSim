using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] List<State> mStates = new List<State>();
    private uint mCurrentState = 20;

    // Start is called before the first frame update
    void Start()
    {
        var states = GetComponentsInChildren<State>();
        for (int i = 0; i < states.Length; ++i)
            mStates.Add(states[i]);

        Debug.AssertFormat(mStates.Count > 0, "No states in state machine");
    }

    // Update is called once per frame
    void Update()
    {
        if (mCurrentState == 20)
        {
            mCurrentState = 0;
            mStates[(int)mCurrentState].Enter();
        }
        mStates[(int)mCurrentState].MyUpdate();
    }
    void OnDrawGizmos()
    {
        if(mCurrentState != 20)
            mStates[(int)mCurrentState].DebugDraw();
    }

    public void ChangeState(string name)
    {
        if(mCurrentState != 20)
        {
            mStates[(int)mCurrentState].Exit();
        }

        for(int i = 0; i < mStates.Count; ++i)
        {
            if(mStates[i].GetName() == name)
            {
                mCurrentState = (uint)i;
                mStates[(int)mCurrentState].Enter();
                break;
            }
        }
    }
}
