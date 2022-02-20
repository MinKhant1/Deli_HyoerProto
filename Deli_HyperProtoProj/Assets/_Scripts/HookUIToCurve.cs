using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookUIToCurve : MonoBehaviour
{


    public Transform target;

    private Camera _cam;

    private BendingManager wcm;
    private float _Curvature;


    public Vector3 offset;

   
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;

        wcm=FindObjectOfType<BendingManager>();
        _Curvature = wcm.bendingAmount;


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 absTarg = target.position + offset;
        Vector3 v = absTarg - _cam.transform.position;
        Vector3 vv = new Vector3(absTarg.x, (v.z * v.z) * -_Curvature + absTarg.y, absTarg.z);
        transform.position = vv;
    }
}
