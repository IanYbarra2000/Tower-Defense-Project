using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed = 10f;
    public GameObject explosionSprite;
    public float deathTime=.5f;
    private Transform target;

    private Animator anim;
    private int wavepointIndex = 0;
    public int health = 1;
    private bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(alive){
            Move();
        }
    }

    void Move(){
        Vector2 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle+90f, Vector3.forward);
        if (Vector2.Distance(transform.position, target.position) <= .1f)
        {
            GetNextWaypoint();
        }
    }
    void GetNextWaypoint()
    {
        if(wavepointIndex>= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    public bool isAlive()
    {
        return alive;
    }

    public void Hit(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            death();
        }
    }

    public void death(){
        
        alive = false;
        GetComponent<BoxCollider2D>().enabled = false;//disable future collisions

        GameObject expl = (GameObject)Instantiate(explosionSprite,transform.position,new Quaternion(0f,0f,0f,0f));

        Destroy(expl,deathTime);
        Destroy(gameObject); //making death instant because of glitch delayed death causes with missed shots
    }
}
