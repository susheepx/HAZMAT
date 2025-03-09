using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : BaseMovementState
{
    public override void EnterState(CharacterMovementManager movement)
    {
        movement.anim.SetBool("Crouching", true);
    }

    public override void UpdateState(CharacterMovementManager movement)
    {
        if(Input.GetKeyDown(KeyCode.LeftShift)) ExitState(movement, movement.Run);
        if(Input.GetKeyDown(KeyCode.C)) 
        {
            if(movement.dir.magnitude < 0.1f) ExitState(movement, movement.Idle);
            else ExitState(movement, movement.Walk);
        }

        if(movement.vInput < 0) movement.currentMoveSpeed = movement.crouchBackSpeed;
        else movement.currentMoveSpeed = movement.crouchSpeed;
                
    }

    void ExitState(CharacterMovementManager movement, BaseMovementState state) {
        movement.anim.SetBool("Crouching", false);
        movement.SwitchState(state);
    }
}
