using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xRange = 3f;
    [SerializeField] float yRange = 3.5f;

    // Start is called before the first frame update
    void Start(){
    }

    void OnEnable() {
        movement.Enable();
    }

    void OnDisable() {
        movement.Disable();
    }

    // Update is called once per frame
    void Update(){
        ProcessRotation();
        ProcessTranslation();
    }

    void ProcessRotation() {
        transform.localRotation = Quaternion.Euler(-30f, 30f, 0f);
    }

    void ProcessTranslation(){
        //new input system
        float xMovement = movement.ReadValue<Vector2>().x;
        float yMovement = movement.ReadValue<Vector2>().y;

        //old input system
        /*
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        */

        Debug.Log(xMovement);
        Debug.Log(yMovement);

        //multiplying by Time.deltaTime makes it fram rate independent
        float xOffset = xMovement * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yMovement * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3 (clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
