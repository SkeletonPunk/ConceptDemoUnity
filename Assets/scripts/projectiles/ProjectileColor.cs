using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class ProjectileColor : MonoBehaviour, IColorable {
	public Projectile projectile;
	Manager.COLORS color = Manager.COLORS.COLORA;
	Material mat;

	void Start() {
		mat = GetComponent<Renderer>().material;
	}

	public Manager.COLORS GetColor() {
		return color;
	}

	public void SetColor(Manager.COLORS newColor) {
		color = newColor;
		mat.color = Manager.GetColor(newColor);
	}
}
