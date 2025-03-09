using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseMovementState
{
    public override void EnterState(CharacterMovementManager movement)
    {
        movement.anim.SetBool("Running", true);
    }

    public override void UpdateState(CharacterMovementManager movement)
    {
        if(Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.Walk);
        else if (movement.dir.magnitude < 0.1f) ExitState(movement, movement.Idle);

        if(movement.vInput < 0) movement.currentMoveSpeed = movement.runBackSpeed;
        else movement.currentMoveSpeed = movement.runSpeed;
    }

    void ExitState(CharacterMovementManager movement, BaseMovementState state) {
        movement.anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
