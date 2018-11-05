using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(SpriteRenderer))]
public class Slime : MonoBehaviour {

    private static readonly WaitForSeconds TIMEOUT = new WaitForSeconds(0.25f);

    private Health health;
    private new SpriteRenderer renderer;
    private Color normalColor;


	private void Awake() {
		health = GetComponent<Health>();
        health.OnDamageTaken += DamageTint;
        renderer = GetComponent<SpriteRenderer>();
        normalColor = renderer.color;
	}

    private void OnDestroy() {
        health.OnDamageTaken -= DamageTint;
    }

    void DamageTint(int damageAmount) {
        StartCoroutine(TintRed());
    }

    private IEnumerator TintRed() {
        renderer.color = Color.red;
        yield return TIMEOUT;
        renderer.color = normalColor;
    }
}
