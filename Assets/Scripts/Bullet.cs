using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public GameObject explosionPrefab;
    public float speed = 15f;
    public float explosionRadius = 0f;
    public int damage = 1;
    //public GameObject explosionRad;
    private Animator anim;
    public void Seek (Transform _target){
        target = _target;
        transform.LookAt(target);
        transform.Rotate(new Vector3(0, 90));
    }
    void Start() {
        anim = gameObject.GetComponent<Animator>();
        Invoke("Die", 10f); //eventual destruction
    }

    // Update is called once per frame
    void Update()
    {
        if (target!=null)
        {
            transform.LookAt(target);
            transform.Rotate(new Vector3(0, 90));       //note: No way to specify the direction of the sprite in Unity, so the 'forward'
                                                        //for the GameObject is not the 'forward' we want, so we rotate it whenever we change
                                                        //direction to look at the target
        }

        float distanceThisFrame = speed * Time.deltaTime;
        Vector3 dir = transform.TransformDirection(Vector3.left);
        Vector3 delta = dir * distanceThisFrame;
        transform.Translate(delta,Space.World);
  

        /*
        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 180f, Vector3.forward);

        float distanceThisFrame = speed * Time.deltaTime;

        //remove, now being done with colliders
        if (dir.magnitude <= distanceThisFrame)
        {
            //HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        */
        
        

        
    }

    void HitTarget(){
        

        //Debug.Log(target.gameObject);
        if(explosionRadius > 0f){
            explode();
        }
        else {
            Damage(target);
        }
        Destroy(gameObject,.3f);
    }

    void explode(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,explosionRadius);
        //Debug.Log(colliders[0]);
        foreach(Collider2D col in colliders){
            if(col.tag == "Enemy"){
                Damage(col.gameObject.GetComponent<Enemy>());
            }
        }
        showExplosion();
    }

    void Die()
    {
        Destroy(gameObject, 0f);
    }

    void showExplosion(){
        //Debug.Log("animation");
        //anim.SetTrigger("Exploded");
        GameObject expl =  (GameObject)Instantiate(explosionPrefab,transform.position,new Quaternion());
        
        expl.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.5f);
        expl.transform.localScale = new Vector3(explosionRadius*2, explosionRadius*2,0f);
        Destroy(expl,.2f);
    }

    //unused now
    void Damage(Transform enemy){
        Enemy enem = enemy.gameObject.GetComponent<Enemy>();
        enem.death();
    }

    public virtual void Damage(Enemy enemy)
    {
        enemy.Hit(damage);
        //enemy.death();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        //Debug.Log(other.gameObject.tag);
        //if colliding with an enemy
        if (other.gameObject.tag.Equals("Enemy"))
        {
            //tell enemy we got hit
            Damage(other.gameObject.GetComponent<Enemy>());
            if(explosionRadius > 0f)
            {
                explode();
            }
            //destroy ourselves
            Destroy(gameObject, 0f);
        }

    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
