using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : State
{
    private Vector3 target;

    public WanderState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        owner.navigation.targetNode = null;
        owner.movement.Resume();
        // create random target position
        target = owner.transform.position + Quaternion.AngleAxis(Random.Range(1, 360), Vector3.up) * Vector3.forward * Random.Range(5, 15);
    }

    public override void OnExit()
    {
        //
    }

    public override void OnUpdate()
    {
        Debug.DrawLine(owner.transform.position, target, Color.magenta);
        owner.movement.MoveTowards(target);
        if (owner.movement.velocity == Vector3.zero)
        {
            owner.stateMachine.StartState(nameof(IdleState));
        }
    }
}
