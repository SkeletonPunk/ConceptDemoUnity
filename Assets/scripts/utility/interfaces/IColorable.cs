using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IColorable {
	Manager.COLORS GetColor();
	void SetColor(Manager.COLORS color);
}
