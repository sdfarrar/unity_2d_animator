using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour {

    public BoxCollider2D DamageArea;
    public LayerMask DamageMask;

    private void Update() {
        Collider2D hit = Physics2D.OverlapBox(Vector2.one * DamageArea.transform.position + DamageArea.offset, DamageArea.size, DamageArea.transform.rotation.eulerAngles.z, DamageMask);
        if(!hit){ return; }

        Debug.Log("hit " + hit.gameObject.name);
    }

}
