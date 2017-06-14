using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(ColorableMaterial))]
public class Enemy : LivingEntity {
	ColorableMaterial material;

	protected override void Start() {
		base.Start();
		material = GetComponent<ColorableMaterial>();
		material.SetColor(color);
	}
}
