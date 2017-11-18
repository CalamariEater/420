using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile {

    public int dmg;
    public float spd;
    public Rigidbody2D rb;
    private string tag;
    //public float size;

    public Projectile()
    {
        tag = "projectile";
        dmg = 1;
        spd = 1;
    }

    public Projectile(int damage, float speed)
    {
        tag = "projectile";
        dmg = damage;
        spd = speed;
    }
}
