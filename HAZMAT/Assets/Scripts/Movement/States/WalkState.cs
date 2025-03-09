using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : BaseMovementState
{
    public override void EnterState(CharacterMovementManager movement)
    {
        movement.anim.SetBool("Walking", true);
    }

    public override void UpdateState(CharacterMovementManager movement)
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)) ExitState(movement, movement.Run);
        else if (Input.GetKeyDown(KeyCode.C)) ExitState(movement, movement.Crouch);
        else if (movement.dir.magnitude < 0.1f) ExitState(movement, movement.Idle);

        if(movement.vInput < 0) movement.currentMoveSpeed = movement.walkBackSpeed;
        else movement.currentMoveSpeed = movement.walkSpeed;
    }

    void ExitState(CharacterMovementManager movement, BaseMovementState state) {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
