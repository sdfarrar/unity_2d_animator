using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopdownCharacterController2D))]
public class PlayerController : MonoBehaviour {
    public static event Action OnAttackStarted = delegate { };
	public static event Action OnAttackFinished = delegate { };

    // movement config
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster

	public Animator Animator;

	private Vector3 _velocity;
	private float normalizedHorizontalSpeed = 0;
	private float normalizedVerticalSpeed = 0;

	private TopdownCharacterController2D _controller;
	private RaycastHit2D _lastControllerColliderHit;
	private InputState _input = new InputState();
	private Vector2 lastDirection = Vector2.down;
	private string animatorState;


	#region Event Listeners

	void OnControllerCollider(RaycastHit2D hit) {
		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void OnTriggerEnterEvent(Collider2D col) {
		Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
	}


	void OnTriggerExitEvent(Collider2D col) {
		Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
	}

	#endregion


	void Awake() {
		Animator = (Animator==null) ? GetComponent<Animator>() : Animator;
		_controller = GetComponent<TopdownCharacterController2D>();

		// listen to some events for no particular reason
		_controller.OnControllerCollidedEvent += OnControllerCollider;
		_controller.OnTriggerEnterEvent += OnTriggerEnterEvent;
		_controller.OnTriggerExitEvent += OnTriggerExitEvent;
	}


	void Update() {
		_input.Update();

		_velocity.x = normalizedHorizontalSpeed = GetNormalizedInput(_input.xAxis);
		_velocity.y = normalizedVerticalSpeed = GetNormalizedInput(_input.yAxis);

		Vector2 currentDirection = GetDirectionFromMovement(_input);
		if(!Attacking()){
			_controller.Move( _velocity.normalized * runSpeed * Time.deltaTime );
			_velocity = _controller.Velocity;
		}else{
			_controller.Move(currentDirection * 0.0001f); // simulate a slight movement in current direction so we can't attack throw objects
			_velocity = Vector3.zero;
		}
		UpdateAnimation(_velocity, currentDirection, _input.Attack);

		lastDirection = currentDirection;
		_input.Reset();
	}

    private void UpdateAnimation(Vector2 movement, Vector2 direction, bool attackPressed){
        SetAnimatiorFloat("FaceX", direction.x);
        SetAnimatiorFloat("FaceY", direction.y);

        if(attackPressed && !Attacking()){
            OnAttackStarted();
            SetAnimationState("Attack"); return;
        }else if(Attacking() && !IsAttackingDone()){
            return; // wait until attack animation is compeleted
        }else if(Attacking() && IsAttackingDone()){
            OnAttackFinished();
        }

        if(movement==Vector2.zero){
            SetAnimationState("Idle");
        }else{
            SetAnimationState("Walk");
        }
    }

    private void SetAnimationState(string animation) {
        if(Animator==null) {return;}
		animatorState = animation;
		Animator.Play(animation);
    }

	private void SetAnimatiorFloat(string name, float value) {
        if(Animator==null) {return;}
		Animator.SetFloat(name, value);
	}

    private bool Attacking() {
        return animatorState=="Attack";
    }

    private bool IsAttackingDone() {
        return Attacking() && Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

	// If input is nonzero return the Sign of that, otherwise return zero
	private float GetNormalizedInput(float input) {
		return (input!=0) ? Mathf.Sign(input) : 0;
	}

    private Vector2 GetDirectionFromMovement(InputState input) {
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
