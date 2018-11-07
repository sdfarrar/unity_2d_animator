using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public event Action<int> OnDamageTaken = delegate {};
    public event Action<int> OnHealthAdded = delegate {};

    [Range(0, 100)]
    public int MaxHP = 100;
    [Range(0, 100), SerializeField]
    private int currentHP;
    [Range(0f, 2f), SerializeField]
    private float invulnerabilityTime = 0f;

    public bool IsInvulnerable { get; private set; }

	void Awake() {
		currentHP = MaxHP;
	}

    public void ApplyChange(int delta) {

        if(delta>=0){
            currentHP += delta;
            OnHealthAdded(delta);
        }else{
            if(IsInvulnerable){ return; }
            currentHP += delta;
            Debug.Log("OnDamageTaken(" + delta + ")");
            OnDamageTaken(delta);
            StartCoroutine(StartInvulnerability());
        }

        if(currentHP==0){ Die(); }
        Mathf.Clamp(currentHP, 0, MaxHP);
    }

    private IEnumerator StartInvulnerability() {
        float duration = invulnerabilityTime, timer = 0;
        IsInvulnerable = true;

        yield return null; // wait til next frame to start counting
        while(timer <= duration) {
            timer += Time.deltaTime;// / duration;
            yield return null;
        }
        IsInvulnerable = false;
    }

    private void Die() {
        Destroy(this.gameObject);
    }
	
}
