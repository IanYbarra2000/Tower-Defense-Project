using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    public SpriteRenderer sprite;
    public float speed = 15f;
    public float explosionRadius = 0f;
    //public GameObject explosionRad;
    private Animator anim;
    public void Seek (Transform _target){
        target = _target;
    }
    void Start() {
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle+180f, Vector3.forward);

        float distanceThisFrame = speed * Time.deltaTime;
        
        if(dir.magnitude <= distanceThisFrame){
            HitTarget();
            return;
        }

        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget(){
        

        //Debug.Log(target.gameObject);
        if(explosionRadius > 0f){
            explode();
        }
        else {
            damage(target);
        }
        Destroy(gameObject,.3f);
    }

    void explode(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,explosionRadius);
        Debug.Log(colliders[0]);
        foreach(Collider2D col in colliders){
            if(col.tag == "Enemy"){
                damage(col.transform);
            }
        }
        showExplosion();
    }

    void showExplosion(){
        Debug.Log("animation");
        anim.SetTrigger("Exploded");
        sprite.color = new Color(1f,1f,1f,.5f);
        transform.localScale = new Vector3(explosionRadius*2, explosionRadius*2,0f);
    }
    void damage (Transform enemy){
        Enemy enem = enemy.gameObject.GetComponent<Enemy>();
        enem.death();
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
