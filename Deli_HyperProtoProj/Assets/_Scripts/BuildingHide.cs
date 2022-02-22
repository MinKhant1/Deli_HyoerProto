using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHide : MonoBehaviour
{

     Transform playerTransform;
   public  GameObject currentBuilding;
    Camera cam;
    public float distCalculate;

    Ray castRay;
    RaycastHit castHit;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        playerTransform = transform;
       
    }

    // Update is called once per frame
    void Update()
    {
        castRay = new Ray(cam.transform.position, playerTransform.position - cam.transform.position);

        distCalculate = Vector3.Distance(playerTransform.position,cam.transform.position);

        if(Physics.Raycast(castRay,out castHit, distCalculate))
        {
            if (castHit.collider != null)
            {
                if (castHit.collider.CompareTag("Building"))
                {
                    if (currentBuilding != null)
                    {
                        if (currentBuilding != castHit.collider.gameObject)
                        {
                            enableBuilding(true);
                            currentBuilding = castHit.collider.gameObject;
                            enableBuilding(false);
                        }
                    }
                    else
                    {
                        currentBuilding = castHit.collider.gameObject;
                        enableBuilding(false);
                    }
                }
                else
                {
                    if (currentBuilding != null)
                    {
                        enableBuilding(true);
                        currentBuilding = null;
                    }
                }
            }
        }
    }

    public void enableBuilding(bool boolOperation)
    {
        if(currentBuilding.transform.parent!=null && currentBuilding.transform.parent.CompareTag("Building"))
        {
            for(int i = 0; i < currentBuilding.transform.parent.childCount; i++)
            {
                MeshRenderer buildingMesh = currentBuilding.transform.parent.GetChild(i).GetComponent<MeshRenderer>() ?? null;

                if (buildingMesh != null)
                {
                    buildingMesh.enabled = boolOperation;
                }
            }
        }
        else if (currentBuilding.transform.childCount < 0)
        {
            for(int i = 0; i < currentBuilding.transform.childCount; i++)
            {
                MeshRenderer buildinMesh = currentBuilding.transform.GetChild(i).GetComponent<MeshRenderer>() ?? null;
                if (buildinMesh != null)
                {
                    buildinMesh.enabled = boolOperation;
                }
            }
        }
        else
        {
            MeshRenderer buildingMesh = currentBuilding.GetComponent<MeshRenderer>() ?? null;
            if (buildingMesh != null)
            {
                buildingMesh.enabled = boolOperation;
            }
        }
    }
}
