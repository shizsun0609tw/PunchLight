using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
   
    public float lifeTime;
    public float speed;
    private Rigidbody rb;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     Destroy(gameObject, lifeTime);
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     rb.velocity = transform.forward * speed;
    // }

    private void OnCollisionEnter(Collision other)
    {
       
    }
    public void EffectCreate()
    {

    }
}
