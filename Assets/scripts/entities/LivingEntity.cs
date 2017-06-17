using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class LivingEntity : MonoBehaviour, IDamagable, IColorable {
	public float defaultHealth = 5F;
	public float deathDelay = 0F;
	protected float health;
	public float currentHealth {
		get{
			return health;
		}
	}

	protected bool isDead = false;
	public event System.Action OnDeath;

	public Manager.COLORS color;
	protected Material mat;

	protected virtual void Start() {
		health = defaultHealth;
		mat = GetComponent<Renderer>().material;
	}

	public virtual float getDefaultHealth() {
		return defaultHealth;
	}

	public virtual float getHealth() {
		return health;
	}

	public virtual void TakeHit(float damageDefault, float damageMatch, RaycastHit hit, Manager.COLORS hitColor) {
		Renderer r = hit.collider.GetComponent<Renderer>();
		if (r && hitColor == color) TakeDamage(damageMatch);
		else TakeDamage(damageDefault);
	}

	public virtual void TakeDamage(float damage){
		health -= damage;

		if (health <= 0 && !isDead) {
			health = 0;
			Die();
		}
	}

	public virtual Manager.COLORS GetColor() {
		return color;
	}

	public virtual void SetColor(Manager.COLORS newColor) {
		color = newColor;
	}

	[ContextMenu("Self Destruct")]
	public virtual void Kill() {
		TakeDamage(health);
	}

	protected void TriggerDeathEvent() {
		if (OnDeath != null) OnDeath();
	}

	protected virtual void Die() {
		isDead = true;
		DeathEvent();
		GameObject.Destroy (gameObject, deathDelay);
	}

	protected void DeathEvent(){
		if (OnDeath != null) OnDeath();
	}
}
