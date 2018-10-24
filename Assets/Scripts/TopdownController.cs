using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownController : MonoBehaviour {

    public Animator Animator;

    private Vector2 lastDirection = Vector2.down;
    private string animatorState;

    public void Move(Vector2 movement, PlayerInputControl.InputState input) {
        string animControllerName = Animator.runtimeAnimatorController.name;

        Vector2 currentDirection = GetDirectionFromMovement(input);
        UpdateAnimation(movement, currentDirection, input.Attack);

        lastDirection = currentDirection;
    }

    private void UpdateAnimation(Vector2 movement, Vector2 direction, bool attackPressed){
        Animator.SetFloat("FaceX", direction.x);
        Animator.SetFloat("FaceY", direction.y);

        if(attackPressed && !Attacking()){
            SetAnimationState("Attack"); return;
        }else if(Attacking() && !IsAttackingDone()){
            return; // wait until attack animation is compeleted
        }

        if(movement==Vector2.zero){
            SetAnimationState("Idle");
        }else{
            SetAnimationState("Walk");
        }
    }

    private void SetAnimationState(string name) {
        animatorState = name;
        Animator.Play(name);
    }

    private bool Attacking() {
        return animatorState=="Attack";
    }

    private bool IsAttackingDone() {
        return Attacking() && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

    private Vector2 GetDirectionFromMovement(PlayerInputControl.InputState input) {
        if(Attacking()){ return lastDirection; } // prevent player from turning when attacking

        // Only return a new direction if it's the only direction being held
        if(input.IsOnlyUpHeld()){
            return Vector2.up;
        }else if(input.IsOnlyDownHeld()){
            return Vector2.down;
        }else if(input.IsOnlyLeftHeld()){
            return Vector2.left;
        }else if(input.IsOnlyRightHeld()){
            return Vector2.right;
        }

        return lastDirection;
    }

}
