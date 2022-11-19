using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaBehaviour : MonoBehaviour
{
    public void shoot()//to use on animation event
    {
        GameObject _bullet = Instantiate(GameAssets.i.projectile,transform.position,transform.rotation);
        _bullet.transform.rotation = transform.rotation;           
    }

    public void ShootLandR()
    {
        for (int i = 0; i < 3; i++)
        {
            Quaternion _rot = transform.rotation;            
            Vector3 _r = new Vector3(0,transform.rotation.z);
            GameObject _bullet = Instantiate(GameAssets.i.projectile,transform.position,transform.rotation);

            _rot.eulerAngles += new Vector3(0,0,90 * -i);
            _bullet.transform.rotation = _rot;
        }
    }

    public void ShootGunShoot()
    {
        for (int i = 0; i < 8; i++)
        {
            Quaternion _rot = transform.rotation;            
            Vector3 _r = new Vector3(0,transform.rotation.z);
            GameObject _bullet = Instantiate(GameAssets.i.projectile,transform.position,transform.rotation);

            _rot.eulerAngles += new Vector3(0,0,20 * -i);
            _bullet.transform.rotation = _rot;
        }
    }

}
