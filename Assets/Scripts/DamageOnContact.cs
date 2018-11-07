using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour {

    [Range(1, 100)]
    public int DamageAmount = 20;
    public BoxCollider2D DamageArea;
    public LayerMask DamageMask;

    private Collider2D[] colliders = new Collider2D[1];

    private void Update() {
        Vector2 point = new Vector2(DamageArea.transform.position.x, DamageArea.transform.position.y) + DamageArea.offset;
        int hits = Physics2D.OverlapBoxNonAlloc(point, DamageArea.size, DamageArea.transform.rotation.eulerAngles.z, colliders, DamageMask);
        if(hits==0){ return; }

        Collider2D hit = colliders[0];
        Health health = hit.transform.root.GetComponent<Health>();
        if(!health){ return ; }

        health.ApplyChange(-DamageAmount);
    }

}
