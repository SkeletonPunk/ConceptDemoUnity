using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(GunController))]
public class DemoGunSwitch : MonoBehaviour{
	public Gun[] guns;
	GunController gunController;
	int index;

	void Start() {
		gunController = GetComponent<GunController>();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift)) {
			index++;
			if (index > guns.Length - 1) index = 0;
		}
		if (!gunController.currentGun || gunController.currentGun.gunName != guns[index].gunName) gunController.EquipGun(guns[index]);
	}
}
