using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    
    void Awake(){
        if(instance!= null)
            Debug.LogError("More than one BuildManager");

        instance = this;
        //moneyDisplay.text = "$"+money;
    }
    
    public GameObject standardTurretPrefab;
    public GameObject explosiveTurretPrefab;

    public void buildTurretOn(Vector3 position){
        if(canBuild){
            if(PlayerStats.Money < turretToBuild.cost){
                Debug.Log("not enough money");
                return;
            }

            PlayerStats.Money -= turretToBuild.cost;
            GameObject turretPlaced = (GameObject)Instantiate(turretToBuild.prefab,position,new Quaternion(0f,0f,0f,0f));
            Debug.Log("Money="+PlayerStats.Money);
        }
        
        
    }
    
    public bool canBuild { get { return turretToBuild != null;}}
    public bool hasMoney { get { return PlayerStats.Money >= turretToBuild.cost;}}
    public void setTurretToBuild(TurretBlueprint turret){
        turretToBuild=turret;
    }

    
}
