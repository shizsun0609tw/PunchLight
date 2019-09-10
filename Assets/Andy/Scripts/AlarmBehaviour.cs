using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmBehaviour : MonoBehaviour
{
    public Transform lightChild;
    public bool OnOff = false;
    private Vector3 defaultRot;
    private Vector3 rightRotLimit, leftRotLimit, targetRot;

    // Start is called before the first frame update
    void Start()
    {
        lightChild.gameObject.SetActive(false);
        defaultRot = transform.rotation.eulerAngles;
        rightRotLimit = defaultRot + Vector3.up*30;
        leftRotLimit = defaultRot - Vector3.up*30;
        targetRot = rightRotLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if(OnOff)
        {
            lightChild.gameObject.SetActive(true);
            if(transform.rotation.eulerAngles.y >= rightRotLimit.y - 5)
            {
                targetRot = leftRotLimit;
            }
            else if(transform.rotation.eulerAngles.y <= leftRotLimit.y + 5)
            {
                targetRot = rightRotLimit;
            }

            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, targetRot, Time.deltaTime));
        }
    }
}
