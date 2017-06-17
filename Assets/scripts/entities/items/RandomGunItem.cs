using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGunItem : Item {
	public Gun[] guns;

	protected override void OnPickup (Collider coll) {
		GunController gunController = coll.GetComponent<GunController>();
		if (gunController) {
			gunController.EquipGun(guns[Random.Range(0, guns.Length)]);
			Destroy(gameObject);
		}
	}
}
