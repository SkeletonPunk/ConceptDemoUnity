using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Collider))]
public abstract class Item : MonoBehaviour {
	public LayerMask validTakers;

	void Start () {
		GetComponent<Collider>().isTrigger = true;
	}

	protected abstract void OnPickup(Collider coll);
	
	void OnTriggerEnter(Collider coll) {
		if (((1 << coll.gameObject.layer) & validTakers) != 0) {
			OnPickup(coll);
		}
	}
}
