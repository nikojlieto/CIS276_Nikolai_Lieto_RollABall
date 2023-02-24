using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPlacerController : MonoBehaviour
{
    [SerializeField]
    public GameObject[] placeableObjects;
    [SerializeField]
    private LayerMask LayerMask;

    private Camera camera;
    private GameObject currentBuildableObject;
    private bool isBuilding;
    

    private void Start(){
        camera = Camera.main;
    }

    private void Update(){
        
        if(Input.GetKeyDown(KeyCode.B)){
            //
            if(currentBuildableObject == null){
                currentBuildableObject = Instantiate(placeableObjects[0]);
            }
            if(isBuilding){
                if(currentBuildableObject.TryGetComponent(out Buildable buildable)){
                    buildable.Build();
                    currentBuildableObject = null;
                }
                
            }
            isBuilding = !isBuilding;
        }
        if(!isBuilding){
            return;
        }

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, LayerMask)){
            if(currentBuildableObject.TryGetComponent(out Buildable buildable)){
                currentBuildableObject.transform.position = hitInfo.point + buildable.heightOffset*Vector3.up;
            //Instantiate(placeableObjects[0], hitInfo.point, Quaternion.identity);
            }
            
        }
    }
}
