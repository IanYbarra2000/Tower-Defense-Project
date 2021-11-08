using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tobor : Turret
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    void UpdateTarget(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach( GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && enemy.GetComponent<Enemy>().isAlive()&&!enemy.GetComponent<Enemy>().slowed)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if(nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
            }
            else
            {
                target = null;
            }
        }
    }
}
