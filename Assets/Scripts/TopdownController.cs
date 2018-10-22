using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownController : MonoBehaviour {

    public Animator Animator;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Move(Vector2 movement) {
        Debug.Log(movement.x + ", " + movement.y);
        Animator.SetFloat("MoveX", movement.x);
        Animator.SetFloat("MoveY", movement.y);
        Animator.SetBool("IsMoving", movement!=Vector2.zero);

        Vector2 normalizedMove = movement.normalized;
    }
}
