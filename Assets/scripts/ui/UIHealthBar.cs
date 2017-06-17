using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class UIHealthBar : MonoBehaviour {
	LivingEntity entity;
	Image health;
	float currentHealth;
	Manager.COLORS currentColor;
	IEnumerator currentColorSwitch;

	void Start () {
		health = GetComponent<Image>();
	}

	void Update () {
		
	}

	IEnumerator ColorSwitch() {
		yield return null;
	}
}
