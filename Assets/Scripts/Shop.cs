using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager bm;
    void Start(){
        bm = BuildManager.instance;
    }
    // Start is called before the first frame update
   public void purchaseStandardTurret(){
       Debug.Log("purchased standard turret");
       bm.setTurretToBuild(bm.standardTurretPrefab);
   }
   public void purchaseExplosiveTurret(){
       Debug.Log("purchases explosive turret");
       bm.setTurretToBuild(bm.explosiveTurretPrefab);
   }
}
