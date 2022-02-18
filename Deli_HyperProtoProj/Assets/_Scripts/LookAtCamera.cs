using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
   
    Transform _cameraTransform;
    Transform _localTransform;



    
    private void Start()
    {
        _cameraTransform = Camera.main.transform;
       

    }

    private void Update()
    {
        transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward, _cameraTransform.rotation * Vector3.up);
    }


}
