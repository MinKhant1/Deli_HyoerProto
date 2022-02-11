using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMovementController : MonoBehaviour
{

    PI playerInput;



    private void Awake()
    {
        playerInput = new PI();
        playerInput.CharacterMovement.Move.started += context => { };


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        playerInput.CharacterMovement.Enable();
    }
    private void OnDisable()
    {
        playerInput.CharacterMovement.Disable();
    }
}
