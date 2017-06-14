using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorParent : MonoBehaviour, IColorChildren {
	Manager.COLORS color;
	List<IColorable> children = new List<IColorable>();

	public void Init(Manager.COLORS color) {
		for (int i = 0; i < transform.childCount; i++) {
			IColorable[] subChildren = transform.GetChild(i).GetComponentsInChildren<IColorable>();
			if (subChildren != null) {
				foreach (IColorable child in subChildren) children.Add(child);
			}
		}
		SetColor(color);
	}

	public void SetChildren() {
		if (children != null) {
			foreach (IColorable child in children) child.SetColor(color);
		}
	}

	public Manager.COLORS GetColor() {
		return color;
	}

	public void SetColor(Manager.COLORS newColor) {
		color = newColor;
		SetChildren();
	}
}
