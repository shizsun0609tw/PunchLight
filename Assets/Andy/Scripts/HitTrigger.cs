using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTrigger : MonoBehaviour
{
    public List<Collider> inTriggerEnemys;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            return;

        if(inTriggerEnemys.Contains(other))
            return;

        inTriggerEnemys.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if(!inTriggerEnemys.Contains(other))
            return;

        inTriggerEnemys.Remove(other);
    }
}
