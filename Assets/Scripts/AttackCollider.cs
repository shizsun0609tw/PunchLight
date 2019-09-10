using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    void OnTriggerStay(Collider other)
    {
        playerController.judgeTriggerByChild(other, PlayerController.JudgeCollider.Attack);
    }
}
