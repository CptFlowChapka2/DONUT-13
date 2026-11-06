using System;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : ForceApplier
{
    private HashSet<Bullet> bullets = new HashSet<Bullet>();
    
    private void OnTriggerEnter(Collider other)
    {
        bullets.Add(CollidersHolder.FindBulletByCollider(other));
    }

    private void FixedUpdate()
    {
        foreach (Bullet bullet in bullets)
        {
            Vector3 direction = (transform.position - bullet.transform.position) * Time.fixedDeltaTime;
            if (!bullet.forces.TryAdd(this, direction)) //Ajoute une fois, puis ensuite ne fait que changer la value
            {
                bullet.forces[this] = direction;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CollidersHolder.FindBulletByCollider(other).forces.Remove(this);
        bullets.Remove(CollidersHolder.FindBulletByCollider(other));
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     var dist = transform.position - other.transform.position;
    //     force = dist;
    //     other.attachedRigidbody ?.AddForce(dist,ForceMode.Force);
    // }
}
