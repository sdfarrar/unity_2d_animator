using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [Range(1, 50)]
    public int DamageAmount = 20;
	public BoxCollider2D Hurtbox;
	public LayerMask HitMask;

	private Collider2D[] colliderHits = new Collider2D[10];

	///<summary>
	/// Keeps track of objects hit during a single attack cycle. Used to prevent hitting
	/// objects multiple times until the cycle is complete as well as any objects that may
	/// contain multiple colliders for its hitbox
	///</summary>
	private HashSet<GameObject> objectsHitThisCycle = new HashSet<GameObject>();

    private void Awake() {
        PlayerController.OnAttackStarted += Enable;
        PlayerController.OnAttackFinished += Disable;
    }

    private void OnDestroy() {
        PlayerController.OnAttackStarted -= Enable;
        PlayerController.OnAttackFinished -= Disable;
    }

	private void Update() {
		if(!Hurtbox.enabled){ return; }

		Vector3 pos = (Vector2.one * Hurtbox.transform.position) + Hurtbox.offset; // multiplication just converts Vec3 to Vec2
		float angle = Hurtbox.transform.rotation.eulerAngles.z;
		int hits = Physics2D.OverlapBoxNonAlloc(pos, Hurtbox.size, angle, colliderHits, HitMask);

		for(int i=0; i<hits; ++i){
			GameObject go = colliderHits[i].gameObject;
			if(objectsHitThisCycle.Contains(go)){ continue; } // ignore, we've already hit this

			objectsHitThisCycle.Add(go);
            Damage(go);
		}
	}

    private void Damage(GameObject go) {
        Health health = go.transform.root.GetComponent<Health>();
        if(health==null){ return; }
        health.ApplyChange(-DamageAmount);
    }

    private void Enable() {
		this.enabled = true;
		Hurtbox.gameObject.SetActive(true);
	}

    private void Disable() {
		this.enabled = false;
		objectsHitThisCycle.Clear();
		Hurtbox.gameObject.SetActive(false);
	}

}
