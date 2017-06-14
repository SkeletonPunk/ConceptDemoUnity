using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {
	public ColorParent target;
	ColorParent currentTarget;
	public float respawnTime;
	float nextSpawnTime;
	bool canResetTime;

	void Update () {
		if (!currentTarget) {
			if (canResetTime) {
				nextSpawnTime = Time.time + respawnTime;
				canResetTime = false;
			}
			if (Time.time > nextSpawnTime) {
				Manager.COLORS color = Manager.GameColors.RandomColor;
				currentTarget = Instantiate<ColorParent>(target, transform.position, transform.rotation, transform);
				currentTarget.Init(color);
				canResetTime = true;
			}
		}
	}
}
