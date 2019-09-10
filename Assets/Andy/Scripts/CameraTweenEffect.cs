using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraTweenEffect : MonoBehaviour
{
    public static CameraTweenEffect Instance;
    private Vector3 defaultPos;

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
        defaultPos = transform.position;
    }

    public void CameraShake(float duration, float strength, int vibrato, float rand)
    {
        transform.DOShakePosition(duration, strength, vibrato, rand, false, true);
    }
    private void InitialPos()
    {
        transform.position = defaultPos;
    }
}
