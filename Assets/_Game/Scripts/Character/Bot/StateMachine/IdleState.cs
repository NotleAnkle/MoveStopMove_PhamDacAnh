using _Framework.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    private float timer;
    public void OnEnter(Bot t)
    {
        t.IsAttackable = true;
        t.ChangeAnim(Constant.ANIM_IDLE);
    }

    public void OnExecute(Bot t)
    {
        timer += Time.deltaTime;

        if(timer > 1f)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {
        
    }
}
