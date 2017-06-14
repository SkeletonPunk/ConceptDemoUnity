using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHealthBar : MonoBehaviour {
	public LivingEntity entity;
	public Transform HealthBarInner;

	void Update () {
		HealthBarInner.localScale = new Vector3(
			(float)entity.currentHealth / (float)entity.defaultHealth,
			HealthBarInner.localScale.y,
			HealthBarInner.localScale.z
		);
		transform.LookAt(Camera.main.transform);
	}
}
