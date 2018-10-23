using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopdownController))]
public class PlayerInputControl : MonoBehaviour {

    public float MoveSpeed = 40f;

    private TopdownController character;
    private float moveX;
    private float moveY;

    private InputState inputState = new InputState();

    private void Awake() {
        character = GetComponent<TopdownController>();
    }
	
	private void Update() {
        moveX = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        moveY = Input.GetAxisRaw("Vertical") * MoveSpeed;
        inputState.Update();
	}

    private void FixedUpdate() {
        Vector2 movement = new Vector2(moveX, moveY) * Time.fixedDeltaTime;
        character.Move(movement, inputState);
    }

    public struct InputState {
        public KeyState W,A,S,D;
        public KeyState Up,Down,Left,Right;
        public float xAxis;
        public float yAxis;

        public void Update() {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");
            Up = W = new KeyState(KeyCode.W);
            Left = A = new KeyState(KeyCode.A);
            Down = S = new KeyState(KeyCode.S);
            Right = D = new KeyState(KeyCode.D);
        }
    }

    public struct KeyState {
        public bool Down;
        public bool Held;

        public KeyState(KeyCode code){
            Down = Input.GetKeyDown(code);
            Held = Input.GetKey(code);
        }
    }

}
