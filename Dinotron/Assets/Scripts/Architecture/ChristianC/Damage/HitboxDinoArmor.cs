﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class HitboxDinoArmor : MonoBehaviour, IHitbox {

    public float health = 0;
    public float maxHealth = 200;
    public float debrisTime = 4f;
    public Transform coveredPart;

    private Transform tr;
    private new Collider collider;
    private new Rigidbody rigidbody;
    private new Renderer renderer;
    private Material material;
    private Color startingColor;

    public event Action<float> DamageTaken;

    // Use this for initialization
    void Start() {
        health = maxHealth;

        tr = transform;
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();

        //If the armor's still whole, it better not go anywhere.
        rigidbody.isKinematic = true;

        renderer = GetComponent<Renderer>();
        material = renderer.material;
        startingColor = material.color;
    }

    public int GetHitPriority() {
        return 0;
    }

    public bool CanBeHit(DamageDealer damageDealer) {
        return health > 0;
    }

    public void TakeDamage(float damage) {

        health -= damage;
        float t = (1 - (health / maxHealth)) * 0.5f;
        material.color = Color.Lerp(startingColor, Color.black, t);

        if (health <= 0) {
            //Go! Be a physics object! Be free!
            collider.isTrigger = false;
            rigidbody.isKinematic = false;
            tr.parent = null;
            gameObject.layer = LayerMask.NameToLayer("Can't Be Hit");

            //Pop off!
            rigidbody.AddForce((tr.position - coveredPart.position).normalized * 10f, ForceMode.VelocityChange);

            //And poof after a while.
            StartCoroutine(Dissapear());
        }

        if (DamageTaken != null) {
            DamageTaken(damage);
        }
    }

    private IEnumerator Dissapear() {
        yield return new WaitForSeconds(debrisTime);
        Destroy(gameObject);
    }
}
