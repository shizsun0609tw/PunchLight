using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public Transform suckLockPoint;
    public float moveSpd;
    public float rotSpd;
    public float atkForce;
    public float suckForce;
    public float suckDistance;
    public HitTrigger hitTrigger;
    public HitTrigger suckTrigger;
    private Vector3 currentDir;
    private bool isMoving = false;
    private Tweener playerWalkTween;

    private Rigidbody rb;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void ScaleOri()
    {
        transform.DOScaleX(1.5f, 0.15f);
        transform.DOScaleY(1.5f, 0.15f);
        isMoving = false;
    }
    private void ScaleDown()
    {
        transform.DOScaleX(1.3f, 0.15f).OnComplete(() => ScaleOri());
        transform.DOScaleY(1.3f, 0.15f);
    }
    private void FixedUpdate()
    {
        MoveFunc();
        AttackFunc();
        SuckFunc();
    }

    private void MoveFunc()
    {
        var h_spd = Input.GetAxis("Horizontal");
        var v_spd = Input.GetAxis("Vertical");
        currentDir  = new Vector3(h_spd, 0, v_spd).normalized;
        if(h_spd != 0 || v_spd != 0)
        {
            rb.velocity = new Vector3(h_spd, 0, v_spd) * moveSpd;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(currentDir), Time.deltaTime * rotSpd);

            if(isMoving == false)
            {
                isMoving = true;
                transform.DOScaleX(1.7f, 0.15f).OnComplete(() => ScaleDown());
                transform.DOScaleY(1.7f, 0.15f);
            }
        }
    }
    private void AttackFunc()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            if(hitTrigger.inTriggerEnemys.Count == 0)
                return;
            
            AudioManager.instance.play("Push");

            // Dictionary<Collider, float> compareDis = new Dictionary<Collider, float>();
            int randIndex = Random.Range(0, hitTrigger.inTriggerEnemys.Count);
            var atkTarget = hitTrigger.inTriggerEnemys[randIndex];
            atkTarget.GetComponent<EnemyExplodeTest>().playerHitted = true;
            atkTarget.GetComponent<Rigidbody>().AddForce(currentDir * atkForce, ForceMode.Impulse);
        }
    }
    private void SuckFunc()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            AudioManager.instance.play("Suck");
            for(int i=0 ; i<suckTrigger.inTriggerEnemys.Count ; i++)
            {
                var suckTarget = suckTrigger.inTriggerEnemys[i];
                if(suckTarget != null)
                {
                    suckEnemy(suckTarget);
                    break;
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.K))
        {
            AudioManager.instance.play("Push");
            StopAllCoroutines();
            AttackSuckEnemys();
        }
    }

    private void AttackSuckEnemys()
    {
        GameObject childEnemy;
        Rigidbody enemyRigid;
        Vector3 enemyTakeDamageDir;

        for (int i = 0; i < suckLockPoint.childCount; ++i)
        {
            childEnemy = suckLockPoint.GetChild(i).gameObject;

            if (childEnemy.tag == "Enemy")
            {
                childEnemy.transform.parent = null;
                enemyRigid = childEnemy.gameObject.GetComponent<Rigidbody>();
                enemyRigid.GetComponent<EnemyExplodeTest>().playerHitted = true;
                enemyTakeDamageDir = transform.forward;
                enemyRigid.AddForce(enemyTakeDamageDir * atkForce, ForceMode.Impulse);
            }
        }
    }

    private void suckEnemy(Collider _enemy)
    {
        StartCoroutine(SuckIng(_enemy));
    }
    IEnumerator SuckIng(Collider _target)
    {
        while(true)
        {
            Rigidbody enemyRigid = _target.gameObject.GetComponent<Rigidbody>();
            Vector3 enemySuckDir = transform.position - _target.transform.position;

            if (Vector3.Distance(_target.transform.position, transform.position) < suckDistance)
            {
                Debug.Log("Stop!!!!");
                // enemyRigid.transform.position = suckLockPoint.transform.position;
                _target.GetComponent<Rigidbody>().velocity = Vector3.one;

                _target.transform.parent = suckLockPoint.transform;
                _target.transform.localPosition = Vector3.zero;
            }
            else 
            {
                // suck enemy to player
                enemyRigid.AddForce(enemySuckDir * suckForce);
            }
            yield return null;
        }
    }
}