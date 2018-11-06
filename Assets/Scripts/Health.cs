﻿using System;
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

	void Awake() {
		currentHP = MaxHP;
	}

    public void ApplyChange(int delta) {
        currentHP += delta;
        Mathf.Clamp(currentHP, 0, MaxHP);
        if(currentHP==0){ Die(); }

        if(delta>=0){
            OnHealthAdded(delta);
        }else{
            Debug.Log("OnDamageTaken(" + delta + ")");
            OnDamageTaken(delta);
        }
    }

    private void Die() {
        Destroy(this.gameObject);
    }
	
}