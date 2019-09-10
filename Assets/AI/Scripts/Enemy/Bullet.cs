using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PooledObject
{
    public Transform hitEffect;
    private Rigidbody rb;

    private float timer = 0;
    public float lifeTime = 2;

    private void Update()
    {
        if (timer >= lifeTime)
            Destroy(gameObject);
        else
            timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("HitPlayer");
            PlayerHealth.Instance.Gethitted();
            ReturnToPool();
        }
        ContactPoint contact = collision.contacts [0];
        Quaternion rot = Quaternion.FromToRotation (Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        var _hiteffect = Instantiate(hitEffect, pos, rot);
        Destroy(_hiteffect.gameObject, 1);
    }
}
