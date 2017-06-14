using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour, IColorChildren {
	public string gunName;
	public float damagePercentDefault;
	public float damagePercentMatch;
	public enum CountMode{Finite, Infinite}
	public CountMode fireMode = CountMode.Finite;
	public CountMode ammoMode = CountMode.Infinite;
	Manager.COLORS color;

	public Transform[] muzzles;
	public Projectile bullet;
	public int shotCount = 1;
	public int ammoCount = 10;
	public float fireCooldown = 0.3F;
	public float bulletVelocity = 35F;
	public float bulletLifetime = 1F;
	int shotsFired = 0;
	int ammoUsed = 0;

	float nextShotTime;
	bool activated;

	List<IColorable> children = new List<IColorable>();

	public virtual void Init() {
		for (int i = 0; i < transform.childCount; i++) {
			IColorable[] subChildren = transform.GetChild(i).GetComponentsInChildren<IColorable>();
			if (subChildren != null) {
				foreach (IColorable child in subChildren) children.Add(child);
			}
		}
		shotsFired = ammoUsed = 0;
	}

	public virtual void Shoot() {
		if (Time.time >= nextShotTime) {
			if (fireMode == CountMode.Finite) {
				if (shotsFired >= shotCount) return;
				shotsFired++;
			}
			if (ammoMode == CountMode.Finite) {
				if (ammoUsed >= ammoCount) return;
				ammoUsed++;
			}

			nextShotTime = Time.time + fireCooldown;
			foreach (Transform muzzle in muzzles) {
				Projectile newBullet = Instantiate<Projectile>(bullet, muzzle.position, muzzle.rotation);
				newBullet.Init(bulletVelocity, bulletLifetime, color, damagePercentDefault, damagePercentMatch);
			}
		}
	}

	public virtual void SetColor(Manager.COLORS newColor) {
		color = newColor;
		SetChildren();
	}

	public Manager.COLORS GetColor() {
		return color;
	}

	public void SetChildren() {
		if (children != null) {
			foreach (IColorable child in children) child.SetColor(color);
		}
	}

	public virtual void OnTriggerHold() {
		Shoot();
	}

	public virtual void OnTriggerRelease() {
		if (fireMode == CountMode.Finite) shotsFired = 0;
	}

	public virtual void OnReload() {
		if (ammoMode == CountMode.Finite) ammoUsed = 0;
	}
}
