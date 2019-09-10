using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorPosing : MonoBehaviour
{
    private Vector3 defaultPos;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
    }

    public void SetPos(Vector3 pos)
    {
        transform.position = defaultPos + pos;
    }
}
