using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileControl : MonoBehaviour
{
    private GameObject turret;
    public GridLayout gridLayout;

    [SerializeField] private Tilemap interactiveMap = null;
    //[SerializeField] private Tilemap pathMap = null;
    [SerializeField] private Tile hoverTile = null;
    //[SerializeField] private RuleTile pathTile = null;
    private Vector3Int previousMousePos = new Vector3Int();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos)) {
            interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
            interactiveMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }

        if(Input.GetMouseButtonDown(0)){
            build();
        }
    }

    void build(){
        if(turret != null){
            Debug.Log("Can't build here");
            return;
        }

        //build Turret
        GameObject turretToBuild = BuildManager.instance.getTurretToBuild();
        Vector3Int cellPosition = interactiveMap.LocalToCell(GetMousePosition());

        GameObject turretPlaced = (GameObject)Instantiate(turretToBuild,cellPosition+new Vector3(.5f,.5f,0f),new Quaternion(0f,0f,0f,0f));   
        
    }
    Vector3Int GetMousePosition () {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return gridLayout.WorldToCell(mouseWorldPos);
    }
}
