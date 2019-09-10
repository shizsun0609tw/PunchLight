using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum JudgeCollider { Attack, Suck};
    public float speed;
    public float rotateSpeed;
    public float attackForce;
    public float suckForce;
    public float suckDistance;

    private bool isAttack;
    private float attackDelay;
    private float goalAngle;
    private Rigidbody rb;
    private string attackAnimation;
    private string idleAnimation;
    private string suckAnimation;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        goalAngle = transform.localEulerAngles.y;
        animator = GetComponent<Animator>();
        attackAnimation = "Attack";
        idleAnimation = "Idle";
        suckAnimation = "Suck";
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        updateState();
        updateRotation();
    }
    
    private void updateState()
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (animatorStateInfo.IsName(idleAnimation))
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.Play(attackAnimation);
                isAttack = true;
                attackDelay = 0.0f;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                animator.Play(suckAnimation);
            }
        }

        if(isAttack && attackDelay < 0.1f)
        {
            attackDelay += Time.deltaTime;
        }
        else
        {
            isAttack = false;
        }

        if (Input.GetMouseButtonUp(1)) attackEnemy();
    }

    private void updateRotation()
    {
        if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
        {
            rb.velocity = new Vector3();
            return;
        }

        if (Mathf.Abs(transform.eulerAngles.y - goalAngle) > 1.0f)
        {
            transform.localRotation 
                = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, goalAngle, 0), rotateSpeed * Time.deltaTime);
        }
        
        setRotation();
        rb.velocity = transform.forward * speed;
    }

    private void setRotation()
    {
        float vertical = -Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        goalAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg + 90;
    }

    public void judgeTriggerByChild(Collider collider, JudgeCollider judgeCollider)
    {
        if (isCollisionEnemy(collider))
        {
            affectEnemy(collider, judgeCollider);
        }
    }

    private bool isCollisionEnemy(Collider enemy)
    {
        return enemy.gameObject.tag == "Enemy";
    }

    private void affectEnemy(Collider enemy, JudgeCollider judgeCollider)
    {
        if (isAttack && judgeCollider == JudgeCollider.Attack && enemy.transform.parent != transform
            && animator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimation))
        {
            isAttack = false;
            attackEnemy(enemy);
        }

        if(judgeCollider == JudgeCollider.Attack && Input.GetMouseButtonUp(1))
        {
            attackEnemy(enemy);
        }
        else if (judgeCollider == JudgeCollider.Suck && Input.GetMouseButton(1))
        {
            suckEnemy(enemy);
        }
    }

    private void attackEnemy()
    {
        GameObject childEnemy;
        Rigidbody enemyRigid;
        Vector3 enemyTakeDamageDir;

        for (int i = 0; i < transform.childCount; ++i)
        {
            childEnemy = transform.GetChild(i).gameObject;
            if (childEnemy.tag == "Enemy")
            {
                childEnemy.transform.parent = null;
                enemyRigid = childEnemy.gameObject.GetComponent<Rigidbody>();
                enemyTakeDamageDir = childEnemy.transform.position - transform.position;
                enemyRigid.AddForce(enemyTakeDamageDir * attackForce);
            }
        }
    }

    private void attackEnemy(Collider enemy)
    {
        Rigidbody enemyRigid = enemy.gameObject.GetComponent<Rigidbody>();
        Vector3 enemyTakeDamageDir = enemy.transform.position - transform.position;
        enemyRigid.AddForce(enemyTakeDamageDir * attackForce);

        attackEnemy();
    }

    private void suckEnemy(Collider enemy)
    {
        Rigidbody enemyRigid = enemy.gameObject.GetComponent<Rigidbody>();
        Vector3 enemySuckDir = transform.position - enemy.transform.position;

        if (Vector3.Distance(enemy.transform.position, transform.position) < suckDistance)
        {
            // enemy stop in front of player
            enemyRigid.velocity = new Vector3();
            enemy.transform.parent = transform;
        }
        else {
            // suck enemy to player
            enemyRigid.AddForce(enemySuckDir * suckForce);
        }
    }

    private void stopEnemy(Collider enemy)
    {
        Rigidbody enemyRigid = enemy.gameObject.GetComponent<Rigidbody>();
        enemyRigid.velocity = new Vector3();
    }
}
