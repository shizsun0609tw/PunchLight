using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 defaulPos;

    // Start is called before the first frame update
    void Start()
    {
        defaulPos = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // transform.position = Vector3.Lerp(transform.position, target.position + defaulPos, Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, target.position + defaulPos, ref velocity, smoothTime);
    }
}
