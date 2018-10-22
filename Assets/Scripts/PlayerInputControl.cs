using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopdownController))]
public class PlayerInputControl : MonoBehaviour {

    public float MoveSpeed = 40f;

    private TopdownController character;
    private float moveX;
    private float moveY;


    private void Awake() {
        character = GetComponent<TopdownController>();
    }
	
	private void Update() {
        moveX = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        moveY = Input.GetAxisRaw("Vertical") * MoveSpeed;
	}

    private void FixedUpdate() {
        Vector2 movement = new Vector2(moveX, moveY) * Time.fixedDeltaTime;
        character.Move(movement);
    }

}
