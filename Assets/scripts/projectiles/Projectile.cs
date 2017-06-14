using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IColorChildren {
	public int bounces = 100;
	public LayerMask targetMask;
	[HideInInspector]
	public float speed = 10F;
	[HideInInspector]
	public float damagePercentDefault;
	[HideInInspector]
	public float damagePercentMatch;

	Manager.COLORS color = Manager.COLORS.COLORA;
	List<IColorable> children = new List<IColorable>();

	bool justSpawned;
	float moveDistance;

	public virtual void Init(float newSpeed, float lifetime, Manager.COLORS newColor, float dPD, float dPM) {
		Destroy(gameObject, lifetime);
		justSpawned = true;
		CheckCollisions(0.001F);

		for (int i = 0; i < transform.childCount; i++) {
			IColorable[] subChildren = transform.GetChild (i).GetComponentsInChildren<IColorable> ();
			if (subChildren != null) {
				foreach (IColorable child in subChildren)
					children.Add (child);
			}
		}

		speed = newSpeed;
		SetColor(newColor);
		damagePercentDefault = dPD;
		damagePercentMatch = dPM;
	}

	protected virtual void Update() {
		moveDistance = speed * Time.deltaTime;
		CheckCollisions(moveDistance);
		transform.Translate(Vector3.forward * moveDistance);
	}

	protected virtual void CheckCollisions(float moveDistance) {
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance, targetMask, QueryTriggerInteraction.Collide)) OnHit(hit);

		if (justSpawned) justSpawned = false;
	}

	protected virtual void OnHit(RaycastHit hit){
		if (hit.collider.gameObject.tag == "RicochetTerrain") {
			if ((justSpawned && hit.distance == 0) || bounces < 1) Destroy(gameObject);
			else {
				bounces--;
				transform.Translate(Vector3.forward * hit.distance);
				moveDistance -= hit.distance;
				transform.forward = Vector3.Reflect(transform.forward, hit.normal);
			}
		}
		else {
			IDamagable entity = hit.collider.GetComponent<IDamagable>();
			if (entity != null) entity.TakeHit(entity.getDefaultHealth() * damagePercentDefault, entity.getDefaultHealth() * damagePercentMatch, hit, color);
			Destroy (gameObject);
		}
	}

	public Manager.COLORS GetColor() {
		return color;
	}

	public virtual void SetColor(Manager.COLORS newColor) {
		color = newColor;
		SetChildren();
	}

	public virtual void SetChildren() {
		if (children != null) {
			foreach (IColorable child in children) child.SetColor(color);
		}
	}
}
