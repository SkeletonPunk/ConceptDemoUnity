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

	public Transform[] muzzles;
	public Projectile bullet;

	public int shotCount = 1;
	public int ammoCount = 10;
	public float fireCooldown = 0.3F;
	public float clickCooldown = 0.1F;
	public float bulletVelocity = 35F;
	public float bulletLifetime = 1F;

	public System.Action gunFinished;

	float nextShotTime;
	bool canClick = true;
	bool isHolding = false;
	Manager.COLORS color;
	int shotsFired = 0;
	int ammoUsed = 0;

	List<IColorable> children = new List<IColorable>();
	IEnumerator currentSet;

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
		foreach (Transform muzzle in muzzles) {
			Projectile newBullet = Instantiate<Projectile>(bullet, muzzle.position, muzzle.rotation);
			newBullet.Init(bulletVelocity, bulletLifetime, color, damagePercentDefault, damagePercentMatch);
		}
		shotsFired++;
		ammoUsed++;
	}

	protected virtual bool GunHasAmmo() {
		if (ammoMode == CountMode.Finite && ammoUsed < ammoCount) return true;
		if (ammoMode == CountMode.Infinite) return true;
		return false;
	}

	protected virtual bool CanShoot() {
		if (fireMode == CountMode.Finite && shotsFired < shotCount) return true;
		if (fireMode == CountMode.Infinite && isHolding) return true;
		return false;
	}

	public virtual IEnumerator ShootSet() {
		bool canContinue = true;
		while (canContinue) {
			Shoot();
			if (GunHasAmmo() && CanShoot())
				yield return new WaitForSeconds (fireCooldown);
			else {
				if (fireMode == CountMode.Finite) shotsFired = 0;
				Invoke("EnableClick", clickCooldown);
				canContinue = false;
				yield return null;
			}
		}

		if (ammoMode == CountMode.Finite && ammoUsed >= ammoCount && gunFinished != null) gunFinished();
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

	public virtual void OnTriggerPress() {
		if (canClick) {
			canClick = false;
			isHolding = true;
			StartCoroutine("ShootSet");
		}
	}

	public virtual void OnTriggerHold() {}

	public virtual void OnTriggerRelease() {
		isHolding = false;
	}

	void EnableClick() {
		canClick = true;
	}
}
