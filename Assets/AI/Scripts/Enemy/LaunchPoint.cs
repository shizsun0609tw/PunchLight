using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPoint : MonoBehaviour
{
    public float speed = 60;

    public float shootSpeed = 10f;

    public Bullet[] prefabs;

    public void Blast(Transform shooter, Transform bulletTrans, int shotNum, int volly, float spread, float shotTime)
    {
        float bulletRot = shooter.eulerAngles.y;    //The y-axis rotation in degrees.
        if (shotNum <= 1)
        {
            //ShootBullet(shooter, bulletRot);
            //Instantiate(bulletTrans, shooter.position, Quaternion.Euler(0, bulletRot, 0));
            SpawnStuff(0, null, new Vector3(0.25f, 0.25f, 0.25f), shooter.position, Quaternion.Euler(0, bulletRot, 0));
        }
        else
        {
            while (volly > 0)
            {
                bulletRot = bulletRot - (spread / 2);       //Offset the bullet rotation so it will start on one side of the z-axis and end on the other.
                for (var i = 0; i < shotNum; i++)
                {
                    //ShootBullet(shooter, bulletRot);
                    //Instantiate(bulletTrans, shooter.position, Quaternion.Euler(0, bulletRot, 0));

                    SpawnStuff(0, null, new Vector3(0.25f, 0.25f, 0.25f), shooter.position, Quaternion.Euler(0, bulletRot, 0));

                    bulletRot += spread / (shotNum - 1);    //Increment the rotation for the next shot.
                }
                bulletRot = shooter.eulerAngles.y; // Reset the default angle.
                volly--;
            }
        }
    }

    public void SpawnStuff(int ID, Transform parent, Vector3 scale, Vector3 position, Quaternion rotation)
    {
        Bullet prefab = prefabs[ID];
        Bullet spawn = prefab.GetPooledInstance<Bullet>();
        spawn.transform.parent = parent;
        spawn.transform.localScale = scale;
        spawn.transform.localPosition = position;
        spawn.transform.localRotation = rotation;

        spawn.GetComponent<Rigidbody>().AddForce(spawn.transform.forward * 10, ForceMode.Impulse);
    }
}
