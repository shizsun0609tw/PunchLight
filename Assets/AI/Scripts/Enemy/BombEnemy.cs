using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BombEnemy : PooledObject
{
    private NavMeshAgent navAgent;

    public GameObject Player;
    [SerializeField]
    private float arriveDistance;
    public float countDownDistance = 10f;

    [SerializeField]
    private float timer;
    public float explodeCD = 5f;

    public float readyCD;
    private bool readyAction = false;

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(Player.transform.position);
        navAgent.stoppingDistance = arriveDistance;
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.SetDestination(Player.transform.position);
        navAgent.stoppingDistance = arriveDistance;
    }

    private void Update()
    {
        if (IsOnShootPosition())
        {
            if (timer >= explodeCD)
            {
                Explode();
            }

            if (timer >= readyCD)
            {
                Ready();
            }

            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }

    private bool IsOnShootPosition()
    {
        navAgent.SetDestination(Player.transform.position);
        navAgent.speed = 10;

        Vector3 myPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 navPos = new Vector3(navAgent.destination.x, 0, navAgent.destination.z);

        if (Vector3.Distance(myPos, navPos) <= countDownDistance)
            return true;
        else
            return false;
    }

    private void Explode()
    {
        Debug.Log("Explode");
        ReturnToPool();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            Explode();
    }

    private void Ready()
    {
        if (!readyAction)
        {
            Debug.Log("Ready");
            readyAction = true;
        }
    }
}
