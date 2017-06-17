using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour {
	public LivingEntity entity;
	[Range (2f, 1000F)]
	public float spawnDelay = 5F;
	[Range (0F, 0.5F)]
	public float spawnDelayVariancePercentage;
	[Range (0f, 1F)]
	public float decreaseFactor;
	[Range (10f, 1000F)]
	public float decreaseFactorRate = 20F;
	public float spawningOffset;
	public float spawnDecreaseOffset;

	void Start () {
		Invoke("StartSpawning", spawningOffset * GetPercentage());
		Invoke("StartDecrease", spawnDecreaseOffset);
	}

	IEnumerator SpawnEnemies() {
		while (true) {
			LivingEntity e = Instantiate<LivingEntity>(entity, transform.position, transform.rotation);
			if (e) e.SetColor(Manager.GameColors.RandomColor);
			yield return new WaitForSeconds(spawnDelay * GetPercentage());
		}
	}

	IEnumerator DecreaseSpawnTime() {
		while (true) {
			spawnDelay -= decreaseFactor;
			if (spawnDelay < 2F) spawnDelay = 2F;
			yield return new WaitForSeconds(decreaseFactorRate);
		}
	}

	void StartDecrease() {
		StartCoroutine("DecreaseSpawnTime");
	}

	void StartSpawning() {
		StartCoroutine("SpawnEnemies");
	}

	float GetPercentage() {
		return Random.Range(1.0F - spawnDelayVariancePercentage, 1.0F + spawnDelayVariancePercentage);
	}
}
