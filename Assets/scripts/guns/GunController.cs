using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour, IColorable {
	Manager.COLORS color;

	public Gun startingGun;
	public Transform weaponHold;
	[HideInInspector]
	public Gun currentGun;

	void Start() {
		if (startingGun) EquipGun (startingGun);
	}

	public void EquipGun(Gun newGun) {
		if (currentGun) Destroy(currentGun.gameObject);
		currentGun = Instantiate<Gun>(newGun, weaponHold.position, weaponHold.rotation);
		currentGun.Init();
		currentGun.transform.parent = weaponHold;
		currentGun.SetColor(color);
	}

	public void OnTriggerHold() {
		if (currentGun) currentGun.OnTriggerHold();
	}

	public void OnTriggerRelease() {
		if (currentGun) currentGun.OnTriggerRelease();
	}

	public void SetColor(Manager.COLORS newColor) {
		color = newColor;
		if (currentGun) currentGun.SetColor(color);
	}

	public Manager.COLORS GetColor() {
		return color;
	}
}
