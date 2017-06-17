using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
[RequireComponent (typeof(Collider))]
public class DemoEnemyA : Enemy {
	public uint points;
	public float defaultDamage;
	public float matchDamage;
	public LayerMask layers;
	public float updateRate;
	NavMeshAgent agent;
	Transform target;
	public uint itemSpawnPercent = 10;
	public Transform healthBar;
	public GameObject[] items;
	uint hits;

	protected override void Start () {
		base.Start();
		agent = GetComponent<NavMeshAgent>();
		GameObject g = GameObject.FindGameObjectWithTag("Player");
		if (g) target = g.transform;
		GetComponent<Collider>().isTrigger = true;
		StartCoroutine("UpdateTarget");
	}

	IEnumerator UpdateTarget() {
		while (target != null) {
			if (agent.isOnNavMesh) agent.SetDestination(new Vector3 (target.position.x, 0, target.position.z));
			yield return new WaitForSeconds(updateRate);
		}
	}

	void OnTriggerEnter(Collider coll) {
		if (((1 << coll.gameObject.layer) & layers) != 0) {
			LivingEntity entity = coll.GetComponent<LivingEntity>();
			if (entity) {
				if (entity.color == color) entity.TakeDamage(matchDamage * entity.defaultHealth);
				else entity.TakeDamage(defaultDamage * entity.defaultHealth);
			}
		}
	}

	public override void TakeDamage(float damage) {
		hits++;
		base.TakeDamage(damage);
	}

	protected override void Die() {
		if(Random.Range(1,101) <= itemSpawnPercent) Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
		Debug.Log (hits);
		if (hits != 0) Manager.score += (points / hits);
		base.Die();
	}
}
