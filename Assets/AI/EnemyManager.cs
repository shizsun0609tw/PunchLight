using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public EnemySpawner[] enemySpawners;

    public int createNum;

    private int killAmount = 0;

    public float timer = 0;
    public float wavesCD = 5f;

    public Text waveText;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }
    }

    private void Start()
    {
        enemySpawners = GameObject.FindObjectsOfType<EnemySpawner>();
        waveText.text = killAmount.ToString();
    }

    private void Update()
    {
        if (timer >= wavesCD)
        {
            for (int i = 0; i < enemySpawners.Length; i++)
            {
                int randomType = Random.Range(0, enemySpawners[i].enemies.Length);
                enemySpawners[i].SpawnStuff(randomType, null, Vector3.one, enemySpawners[i].transform.position, Quaternion.identity);
            }
            timer = 0;
            // CurrentWave += 1;
            // waveText.text = CurrentWave.ToString();
        }

        timer += Time.deltaTime;
    }
    public void AddKilledCount()
    {
        Debug.Log("kill!!!!");
        killAmount ++;
        waveText.text = killAmount.ToString();
        waveText.transform.DOShakeScale(0.5f, 0.5f, 50, 90, true);
    }
}
