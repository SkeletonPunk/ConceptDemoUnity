using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TrailRenderer))]
public class ColorableTrail : MonoBehaviour, IColorable {
	Manager.COLORS color = Manager.COLORS.COLORA;
	TrailRenderer trail;

	void CheckTrailState() {
		if (!trail) {
			trail = GetComponent<TrailRenderer>();
			Manager.OnColorUpdate += ResetColor;
		}
	}

	public Manager.COLORS GetColor(){
		return color;
	}

	public void SetColor(Manager.COLORS newColor) {
		CheckTrailState();
		color = newColor;
		trail.material.SetColor("_TintColor", Manager.GetColor(newColor));
	}

	void ResetColor() {
		CheckTrailState();
		trail.material.SetColor("_TintColor", Manager.GetColor(color));
	}
}
