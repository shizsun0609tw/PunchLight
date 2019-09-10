using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightStatus
{
    Normal,
    Alarm
}
public class GroundLightController : MonoBehaviour
{
    public LightStatus lightStatus;
    public List<LightSnigle> lightGroup;

    // Update is called once per frame
    void Update()
    {
        switch(lightStatus)
        {
            case LightStatus.Normal :
            foreach(var manber in lightGroup)
            {
                manber.NormalLight();
            }
            break;
            case LightStatus.Alarm :
            foreach(var manber in lightGroup)
            {
                manber.AlarmLight();
            }
            break;
        }
    }
}
