using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    

    [Header("Attributes")]
    public float range = 10f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public int price =0;
    public float rotationOffset = 90;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public GameObject[] bulletPrefab;
    
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f,.5f);
        
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach( GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && enemy.GetComponent<Enemy>().isAlive())
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
    // Update is called once per frame
    void Update()
    {
        faceTarget();
        /*
        Quaternion lookRotation = Quaternion.LookRotation(dir,transform.TransformDirection(Vector3.up))*Quaternion.Euler(0,0,-270f);
        transform.rotation = new Quaternion(0, 0, lookRotation.z, lookRotation.w);
        Vector3 rotation = lookRotation.eulerAngles;
        Debug.Log(rotation);
        transform.rotation = Quaternion.Euler(rotation.x,0f,0f);*/

        if(fireCountdown<=0f&&target!= null){
            Shoot();
            fireCountdown = 1f/fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    void faceTarget(){
        if (target == null)
            return;
        
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle+rotationOffset, Vector3.forward);
    }
    void Shoot() {
        int index = Random.Range(0,bulletPrefab.Length);
        anim.SetTrigger("Shooting");

        GameObject bulletGO = (GameObject)Instantiate (bulletPrefab[index], transform.position,transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet!= null){
            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
