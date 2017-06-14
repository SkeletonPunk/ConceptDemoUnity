using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class ChildColorable : MonoBehaviour {
	public Transform ColorableParent;
	IColorable parent;
	Manager.COLORS color = Manager.COLORS.COLORA;
	Material mat;
	bool canColor;

	void Start() {
		mat = GetComponent<Renderer>().material;
		parent = ColorableParent.GetComponent<IColorable>();
		if (parent != null) {
			canColor = true;
			color = parent.GetColor();
			mat.color = Manager.GetColor(color);
		}
		Manager.OnColorUpdate += ResetColor;
	}

	void Update () {
		if (canColor && color != parent.GetColor()) {
			color = parent.GetColor();
			mat.color = Manager.GetColor(color);
		}
	}

	void ResetColor() {
		mat.color = Manager.GetColor(color);
	}
}
