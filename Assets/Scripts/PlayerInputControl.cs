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
        Vector2 movement = new Vector2(inputState.xAxis, inputState.yAxis) * MoveSpeed * Time.fixedDeltaTime;
        character.Move(movement.normalized, inputState);
        inputState.Reset();
    }


    public class InputState {
        public KeyState Up,Down,Left,Right;
        public float xAxis, yAxis;
        public bool Attack;

        public InputState() {
            Up = new KeyState(KeyCode.W);
            Left = new KeyState(KeyCode.A);
            Down = new KeyState(KeyCode.S);
            Right = new KeyState(KeyCode.D);
        }

        public void Update() {
            PollAxes();
            PollMovement();
            PollActions();
        }

        public void Reset() {
            Attack = false;
        }

        public bool IsOnlyUpHeld(){ return (Up.Held || yAxis>0) && !(Right.Held || Left.Held || Down.Held); }
        public bool IsOnlyDownHeld(){ return (Down.Held || yAxis<0) && !(Right.Held || Left.Held || Up.Held); }
        public bool IsOnlyLeftHeld(){ return (Left.Held || xAxis<0) && !(Down.Held || Up.Held || Right.Held); }
        public bool IsOnlyRightHeld(){ return (Right.Held || xAxis>0) && !(Down.Held || Up.Held || Left.Held); }

        private void PollAxes() {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");
        }

        private void PollMovement() {
            Up.Update();
            Left.Update();
            Down.Update();
            Right.Update();
        }

        private void PollActions() {
            if(Attack){ return; }
            Attack = Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Keypad4) || Input.GetMouseButtonDown(0);
        }
    }

    public struct KeyState {
        public bool Down;
        public bool Held;

        KeyCode code;

        public KeyState(KeyCode code){
            this.code = code;
            Down = Held = false;
        }

        public void Update() {
            Down = Down || Input.GetKeyDown(code); // preserve down presses until next reset
            Held = Input.GetKey(code);
        }

        public void Reset(){
            Down = Held = false;
        }
    }

}
