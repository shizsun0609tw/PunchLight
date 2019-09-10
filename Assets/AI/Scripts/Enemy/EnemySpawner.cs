using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public PooledObject[] enemies;
    public int enemyType;

    private float timer = 0;
    public float spawnCD = 5f;

    private void Update()
    {
        /*
        if(timer >= spawnCD)
        {
            timer = 0;
            int randomType = Random.Range(0, enemies.Length);
            SpawnStuff(randomType, null, Vector3.one, transform.position, Quaternion.identity);
        }
        else
        {
            timer += Time.deltaTime;
        }
        */
    }

    public void SpawnStuff(int ID, Transform parent, Vector3 scale, Vector3 position, Quaternion rotation)
    {
        PooledObject prefab = enemies[ID];
        PooledObject spawn = prefab.GetPooledInstance<PooledObject>();
        spawn.transform.parent = parent;
        spawn.transform.localScale = scale;
        spawn.transform.localPosition = position;
        spawn.transform.localRotation = rotation;
    }
}
