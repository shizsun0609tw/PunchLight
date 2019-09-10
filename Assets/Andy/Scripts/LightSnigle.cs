using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSnigle : MonoBehaviour
{
    private Color defaultLightColor;
    private Light selfLight;

    // Start is called before the first frame update
    void Start()
    {
        selfLight = GetComponent<Light>();
        defaultLightColor = selfLight.color;
    }

    public void AlarmLight()
    {
        selfLight.color = Color.red;
    }
    public void NormalLight()
    {
        selfLight.color = defaultLightColor;
    }
}
