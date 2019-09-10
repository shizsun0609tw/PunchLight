using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : PooledObject
{
    public int shotAmount;
    private NavMeshAgent navAgent;

    public GameObject Player;
    [SerializeField]
    private float arriveDistance;

    public float lookDistance;
    public Transform LaunchPoint;
    public GameObject Bullet;

    [SerializeField]
    private float timer;
    public bool canShoot = true;
    public float shootCD;
    public float readyCD;

    public float moveSpeed = 10;
    private bool readyAction = false;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.stoppingDistance = arriveDistance;
    }

    private void OnEnable()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.stoppingDistance = arriveDistance;
        moveSpeed = Random.Range(10,20);
        navAgent.speed = moveSpeed;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position,Player.transform.position) < lookDistance)
        {
            if (IsOnShootPosition())
            {
                if (timer >= shootCD)
                {
                    Shoot();
                }
                else if(timer >= readyCD)
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
    }

    private bool IsOnShootPosition()
    {
        navAgent.SetDestination(Player.transform.position);

        Vector3 myPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 navPos = new Vector3(navAgent.destination.x, 0, navAgent.destination.z);

        if (Vector3.Distance(myPos, navPos) <= arriveDistance)
            return true;
        else
            return false;
    }

    private void Shoot()
    {
        timer = 0;
        readyAction = false;

        if (LaunchPoint.GetComponent<LaunchPoint>() != null)
        {
            //LaunchPoint.GetComponent<LaunchPoint>().SpawnStuff();
            LaunchPoint.GetComponent<LaunchPoint>().Blast(LaunchPoint, Bullet.transform, 20, 1, shotAmount, 0.1f);
        }
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
