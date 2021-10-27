using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;

    public float speed = 15f;

    public void Seek (Transform _target){
        target = _target;
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
        Destroy(gameObject);

        //Debug.Log(target.gameObject);
        Enemy enem = target.gameObject.GetComponent<Enemy>();
        enem.death();
    }
}
