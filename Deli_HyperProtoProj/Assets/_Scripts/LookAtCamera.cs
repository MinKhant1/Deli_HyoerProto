using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Vector3 _normalRotation= new Vector3(-45, 0, 0);
    Vector3 _inversedRotation=new Vector3(45, -180f, 0);


    Transform _cameraTransform;
    Transform _localTransform;



    
    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        _localTransform = GetComponent<Transform>();
        //Vector3 rotation = transform.forward;
        //print(rotation);
        //if (rotation.z > 0)
        //{
        //    transform.Rotate(_normalRotation, Space.World);

        //}
        //else
        //{
        //    transform.Rotate(_inversedRotation, Space.World);
        //}

    }

    private void Update()
    {
        transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.back, _cameraTransform.rotation * Vector3.up);
    }


}
