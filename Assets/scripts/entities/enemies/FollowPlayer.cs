using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class FollowPlayer : MonoBehaviour {

	public float updateRate;
	NavMeshAgent agent;
	Transform target;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		StartCoroutine("UpdateTarget");
	}
	
	IEnumerator UpdateTarget() {
		while (target != null) {
			agent.SetDestination(new Vector3(target.position.x, 0, target.position.z));
			yield return new WaitForSeconds(updateRate);
		}
	}
}
