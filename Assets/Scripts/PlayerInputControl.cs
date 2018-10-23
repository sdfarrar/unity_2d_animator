using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopdownController))]
public class PlayerInputControl : MonoBehaviour {

    public float MoveSpeed = 40f;

    private TopdownController character;
    private InputState inputState = new InputState();

    private void Awake() {
        character = GetComponent<TopdownController>();
    }
	
	private void Update() {
        inputState.Update();
	}

    private void FixedUpdate() {
        float moveX = inputState.xAxis * MoveSpeed;
        float moveY = inputState.yAxis * MoveSpeed;
        Vector2 movement = new Vector2(moveX, moveY) * Time.fixedDeltaTime;
        character.Move(movement, inputState);
    }

    public struct InputState {
        public KeyState W,A,S,D; // Movement states
        public KeyState Up,Down,Left,Right; // Movement states
        public KeyState Attack; // Action states

        public float xAxis;
        public float yAxis;

        public void Update() {
            PollAxes();
            PollMovement();
            PollActions();
        }

        private void PollAxes() {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");
        }

        private void PollMovement() {
            Up = W = new KeyState(KeyCode.W);
            Left = A = new KeyState(KeyCode.A);
            Down = S = new KeyState(KeyCode.S);
            Right = D = new KeyState(KeyCode.D);
        }

        private void PollActions() {
            Attack = new KeyState("Fire1");
        }
    }

    public struct KeyState {
        public bool Down;
        public bool Held;

        public KeyState(KeyCode code){
            Down = Input.GetKeyDown(code);
            Held = Input.GetKey(code);
        }

        public KeyState(string name){
            Down = Input.GetButtonDown(name);
            Held = Input.GetButton(name);
        }
    }

}
