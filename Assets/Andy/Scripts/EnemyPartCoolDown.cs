using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartCoolDown : MonoBehaviour
{
    public Renderer[] childParts;
    private float fadeValue = 0;

    // Update is called once per frame
    void Update()
    {
        fadeValue = Mathf.Lerp(fadeValue, 1, Time.deltaTime);
        foreach ( var manber in childParts)
        {
            manber.material.SetFloat("_HotToCoolBlending", fadeValue);
        }
    }
}
