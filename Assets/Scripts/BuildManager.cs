using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private GameObject turretToBuild;

    void Awake(){
        if(instance!= null)
            Debug.LogError("More than one BuildManager");

        instance = this;
    }
    
    public GameObject standardTurretPrefab;
    void Start(){
        turretToBuild = standardTurretPrefab;
    }
    public GameObject getTurretToBuild(){
        return turretToBuild;
    }
}
