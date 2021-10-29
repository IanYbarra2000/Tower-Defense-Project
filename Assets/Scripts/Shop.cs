﻿using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standard;
    public TurretBlueprint missile;
    BuildManager bm;
    void Start(){
        bm = BuildManager.instance;
    }
    // Start is called before the first frame update
   public void selectStandardTurret(){
       Debug.Log("purchased standard turret");
       bm.setTurretToBuild(standard);
   }
   public void selectExplosiveTurret(){
       Debug.Log("purchases explosive turret");
       bm.setTurretToBuild(missile);
   }
}
