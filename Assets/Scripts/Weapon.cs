using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public BoxCollider2D Hurtbox;
	public LayerMask HitMask;

	private Collider2D[] colliderHits = new Collider2D[10];

	void Update() {
		Vector3 pos = (Vector2.one * Hurtbox.transform.position) + Hurtbox.offset;
		float angle = Hurtbox.transform.rotation.eulerAngles.z;
		int hits = Physics2D.OverlapBoxNonAlloc(pos, Hurtbox.size, angle, colliderHits, HitMask);

		for(int i=0; i<hits; ++i){
			string name = colliderHits[i].gameObject.name;
			Debug.Log("hit: " + name);
		}
	}

}
