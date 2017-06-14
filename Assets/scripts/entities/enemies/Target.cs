using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Enemy {
	public Transform parent;

	protected override void Die() {
		isDead = true;
		TriggerDeathEvent();
		GameObject.Destroy (parent, deathDelay);
	}
}
