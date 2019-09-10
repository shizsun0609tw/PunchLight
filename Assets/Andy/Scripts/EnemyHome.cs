using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHome : MonoBehaviour
{
    public Transform enemyTrans;
    public  float timeToCreate = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeToCreateEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator TimeToCreateEnemy()
    {
        while(true)
        {
            Instantiate(enemyTrans, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeToCreate);
        }
    }
}
