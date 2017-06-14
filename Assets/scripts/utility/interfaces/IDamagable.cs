using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable{
	float getDefaultHealth();

	float getHealth();

	void TakeHit(float damageDefault, float damageMatch, RaycastHit hit, Manager.COLORS hitColor);

	void TakeDamage(float damage);

	void Kill();
}
