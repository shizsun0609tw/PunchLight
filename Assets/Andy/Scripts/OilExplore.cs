using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OilExplore : MonoBehaviour
{
    public Transform effectExplore;
    
    public void ExploreOil()
    {
        AudioManager.instance.play("Bomb");
        var par = Instantiate(effectExplore, transform.position, Quaternion.identity);
        Camera.main.transform.DOShakePosition(0.25f, 0.5f, 50, 90, false, true);
        Destroy(this.gameObject);
        Destroy(par.gameObject, 3);
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag != "Enemy")
            return;

        var test = other.transform.GetComponent<EnemyExplodeTest>();
        
        if(test == null)
            return;
        if(test.playerHitted)
        {
            ExploreOil();
        }
    }
}
