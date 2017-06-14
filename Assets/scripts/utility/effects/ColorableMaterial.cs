using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class ColorableMaterial : MonoBehaviour, IColorable {
	Manager.COLORS color = Manager.COLORS.COLORA;
	Material mat;

	public Manager.COLORS GetColor() {
		return color;
	}

	void CheckMaterialState() {
		if (!mat) {
			mat = GetComponent<Renderer>().material;
			Manager.OnColorUpdate += ResetColor;
		}
	}

	public void SetColor(Manager.COLORS newColor) {
		CheckMaterialState();
		color = newColor;
		mat.color = Manager.GetColor(newColor);
	}

	void ResetColor() {
		CheckMaterialState();
		mat.color = Manager.GetColor(color);
	}
}
