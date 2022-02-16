using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Vector3 _normalRotation= new Vector3(-45, 0, 0);
    Vector3 _inversedRotation=new Vector3(45, -180f, 0);


    
    private void Start()
    {

        Vector3 rotation = transform.forward;
        if (rotation.z > 0)
        {
            transform.Rotate(_normalRotation, Space.World);

        }
        else
        {
            transform.Rotate(_inversedRotation, Space.World);
        }

    }


}
