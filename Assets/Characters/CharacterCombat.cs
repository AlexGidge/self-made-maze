using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterCombat : MonoBehaviour
{
    internal void FireBullet(GameObject bulletPrefab, Quaternion direction)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = this.transform.position;
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bullet.transform.rotation = direction;
            bulletController.FireBullet(direction);
        }
    }
}
