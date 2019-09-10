using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyExplodeTest : MonoBehaviour
{
    public Transform shakeWave;
    public Transform bloodPrefab;
    public GameObject breakStateEnemy; 
    public GameObject normalStateEnemy;
    public bool playerHitted = false;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        normalStateEnemy.SetActive(true);
        breakStateEnemy.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != "Wall")
            return;

        if(playerHitted)
        {
            EnemyDead();
            transform.DetachChildren();
            Destroy(this.gameObject);
            Destroy(normalStateEnemy);
        }
    }

    public void EnemyDead()
    {
        AudioManager.instance.play("HitWall");

        var wave = Instantiate(shakeWave.gameObject, transform.position, Quaternion.identity);
        EnemyManager.Instance.AddKilledCount();
        Destroy(wave, 1f);

        breakStateEnemy.SetActive(true);
        normalStateEnemy.SetActive(false);

        var partRigids = breakStateEnemy.GetComponentsInChildren<Rigidbody>();
        foreach (var part in partRigids)
        {
            part.AddExplosionForce(10, breakStateEnemy.transform.parent.position, 3, 1, ForceMode.Impulse);
            var dir = (part.position - PlayerControl.instance.transform.position).normalized;
            part.AddForce(dir * 10, ForceMode.Impulse);
        }
        var bloodParent = Instantiate(bloodPrefab, new Vector3(transform.position.x, 3, transform.position.z), Quaternion.identity);
        var bloodProjector = bloodParent.GetComponentInChildren<ProJectorSpark>();
    }
}
