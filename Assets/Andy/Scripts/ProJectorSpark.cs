using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProJectorSpark : MonoBehaviour
{
    public Projector projector;
    public Color targetColor;
    private Material projectMat;
    private Color redColor;
    
    private void OnEnable()
    {
        projector.fieldOfView = 0;
        projectMat = projector.material;
        redColor = Color.red;
        projector.material.color = redColor;
    }
    private void Update()
    {
        projector.fieldOfView = Mathf.Lerp(projector.fieldOfView, 230, Time.deltaTime * 15);
        redColor = Color.Lerp(redColor, targetColor, Time.deltaTime * 0.75f);
        projectMat.SetColor("_Color", redColor);
    }
}
