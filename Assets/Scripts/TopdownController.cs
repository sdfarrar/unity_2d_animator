using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownController : MonoBehaviour {

    public Animator Animator;

    private Direction lastDirection;

	// Use this for initialization
	void Start () {
		Debug.Log(Animator.runtimeAnimatorController.name);
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Move(Vector2 movement) {
        string animControllerName = Animator.runtimeAnimatorController.name;
        //Debug.Log(movement.x + ", " + movement.y);

        if(animControllerName=="PlayerController"){
            AnimationV1(movement);
        }else if(animControllerName=="PlayerControllerV2"){
            AnimationV2(movement);
        }else{
            Debug.LogWarning("Unknown Animation Controller: " + animControllerName);
        }

        Vector2 normalizedMove = movement.normalized;
    }

    private void AnimationV1(Vector2 movement){
        Animator.SetFloat("MoveX", movement.x);
        Animator.SetFloat("MoveY", movement.y);
        Animator.SetBool("IsMoving", movement!=Vector2.zero);
    }

    private void AnimationV2(Vector2 movement){
        Animator.SetFloat("FaceX", movement.x);
        Animator.SetFloat("FaceY", movement.y);

        if(movement==Vector2.zero){
            Animator.Play("Idle");
        }else{
            Animator.Play("Walk");
        }
    }

    private Direction GetDirectionFromMovement(Vector2 movement) {
        //TODO make direction from movement to set FaceX and FaceY
        return Direction.Down;
    }

    public enum Direction {
        Up,Down,Left,Right
    }
}
