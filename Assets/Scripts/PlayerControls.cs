using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable() {
        movement.Enable();
    }

    void OnDisable() {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //new input system
        float xMovement = movement.ReadValue<Vector2>().x;
        float yMovement = movement.ReadValue<Vector2>().y;

        Debug.Log(xMovement);
        Debug.Log(yMovement);

        //old input system
        /* float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical"); */
    }
}
