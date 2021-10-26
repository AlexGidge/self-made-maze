using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    public Rigidbody2D rigidBody;

    private bool fired;
    private float bulletSpeed = 5000f;
    private float multiplier = 1;
    [SerializeField] private Quaternion bulletDirection;

    void Awake()
    {
        StartCoroutine(Selfdestruct());
    }

    IEnumerator Selfdestruct()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
    
    public void FireBullet(Quaternion direction)
    {
        bulletDirection = direction;
        fired = true;
    }

    private void FixedUpdate()
    {
        if (fired)
        {
            Vector3 force = bulletDirection * Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(force * bulletSpeed * Time.deltaTime * multiplier );
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (multiplier > 0)
        {
            multiplier -= 0.1f;
        }
    }
}


