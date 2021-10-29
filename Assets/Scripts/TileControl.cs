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
    [SerializeField] private Tilemap pathMap = null;
    [SerializeField] private Tile hoverTile = null;
    //[SerializeField] private RuleTile pathTile = null;
    private Vector3Int previousMousePos = new Vector3Int();
    private Vector3Int mousePos;
    private BuildManager bm;
    // Start is called before the first frame update
    void Start()
    {
        bm = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = GetMousePosition();
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
        if(turret != null || !mouseInBounds()|| onPath()){
            Debug.Log("Can't build here");
            return;
        }

        //build Turret
        Vector3Int cellPosition = interactiveMap.LocalToCell(GetMousePosition());

        //GameObject turretPlaced = (GameObject)Instantiate(turretToBuild,cellPosition+new Vector3(.5f,.5f,0f),new Quaternion(0f,0f,0f,0f));   
        bm.buildTurretOn(cellPosition+new Vector3(.5f,.5f,0f));
        
    }
    Vector3Int GetMousePosition () {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return gridLayout.WorldToCell(mouseWorldPos);
    }

    bool onPath(){
        return pathMap.HasTile(mousePos);
    }
    bool mouseInBounds(){
        Vector3 pos = GetMousePosition();
        return (pos.y<4f&&pos.y>-4f&&pos.x<6f&&pos.x>-8f);
    }
}
