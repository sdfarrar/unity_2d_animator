using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownController : MonoBehaviour {

    public Animator Animator;

    private Vector2 lastDirection = Vector2.down;

    public void Move(Vector2 movement, PlayerInputControl.InputState input) {
        string animControllerName = Animator.runtimeAnimatorController.name;

        Vector2 currentDirection = GetDirectionFromMovement(input);
        if(animControllerName=="PlayerController"){
            AnimationV1(movement);
        }else if(animControllerName=="PlayerControllerV2"){
            AnimationV2(movement, currentDirection, input.Attack.Down);
        }else{
            Debug.LogWarning("Unknown Animation Controller: " + animControllerName);
        }

        Vector2 normalizedMove = movement.normalized;
        lastDirection = currentDirection;
    }

    private void AnimationV1(Vector2 movement){
        Animator.SetFloat("MoveX", movement.x);
        Animator.SetFloat("MoveY", movement.y);
        Animator.SetBool("IsMoving", movement!=Vector2.zero);
    }

    private void AnimationV2(Vector2 movement, Vector2 direction, bool attackPressed){
        Animator.SetFloat("FaceX", direction.x);
        Animator.SetFloat("FaceY", direction.y);

        //TODO allow attack animation to finish before we go back to other states
        if(attackPressed){
            Debug.Log("ATTACK");
            Animator.Play("Attack");
            return;
        }

        if(movement==Vector2.zero){
            Animator.Play("Idle");
        }else{
            Animator.Play("Walk");
        }
    }

    private Vector2 GetDirectionFromMovement(PlayerInputControl.InputState input) {
        // Only return a new direction if it's the only direction being held
        if(input.Up.Held && !(input.Right.Held || input.Left.Held || input.Down.Held)){
            return Vector2.up;
        }else if(input.Down.Held && !(input.Right.Held || input.Left.Held || input.Up.Held)){
            return Vector2.down;
        }else if(input.Left.Held && !(input.Down.Held || input.Up.Held || input.Right.Held)){
            return Vector2.left;
        }else if(input.Right.Held && !(input.Down.Held || input.Up.Held || input.Left.Held)){
            return Vector2.right;
        }

        return lastDirection;
    }

}
