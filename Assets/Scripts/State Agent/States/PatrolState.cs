using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PatrolState : State
{
    private float stateTimer;

    public PatrolState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        owner.movement.Resume();
        owner.navigation.targetNode = owner.navigation.GetNearestNode();
        stateTimer = Random.Range(5, 10);
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0)
        {
            owner.stateMachine.StartState(nameof(WanderState));
        }
        if (owner.perceived.Length > 0)
        {
            owner.stateMachine.StartState(nameof(ChaseState));
        }
    }
}
