using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 10f;

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

        //old input system
        /* float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical"); */

        Debug.Log(xMovement);
        Debug.Log(yMovement);

        //multiplying by Time.deltaTime makes it fram rate independent
        float xOffset = xMovement * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;

        float yOffset = yMovement * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;

        transform.localPosition = new Vector3 
        (newXPos, newYPos, transform.localPosition.z);


    


    }
}
