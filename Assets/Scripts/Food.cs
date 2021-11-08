using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Bullet
{
    
    public float timeSlowed = 1f;
    // Update is called once per frame
    public override void Damage(Enemy enemy){
        enemy.Slow(timeSlowed);
    }
}
