using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [SerializeField] InputAction movement;
    [Tooltip("How fast ship moves up and down based upon player input")] [SerializeField] float controlSpeed = 10f;
    [Tooltip("How fast player moves horizontally")] [SerializeField] float xRange = 3f;
    [Tooltip("How fast player moves vertically")] [SerializeField] float yRange = 6f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")] [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -14f;
    [SerializeField] float positionYawFactor = 7f;

    [Header("Player position based tuning")]
    [SerializeField] float controlPitchFactor = -14f;
    [SerializeField] float controlRollFactor = -20f;

    [SerializeField] InputAction fire;

    float xMovement, yMovement;

    // Start is called before the first frame update
    void Start(){
    }

    void OnEnable() {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable() {
        movement.Disable();
        fire.Disable();
    }

    // Update is called once per frame
    void Update(){
        ProcessRotation();
        ProcessTranslation();
        ProcessFiring();
    }

    void ProcessRotation() {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControl = yMovement * controlPitchFactor;

        float pitch =  pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xMovement * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation(){
        //new input system
        xMovement = movement.ReadValue<Vector2>().x;
        yMovement = movement.ReadValue<Vector2>().y;

        //old input system
        /*
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");
        */

        //multiplying by Time.deltaTime makes it fram rate independent
        float xOffset = xMovement * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yMovement * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3 (clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring() {
        //if pushing firing button
        //then print "shooting"
        //else don't print "shooting"

        if (fire.ReadValue<float>() > 0.5) {
            SetLasersActive(true);
        }
        else {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive) {
        foreach (GameObject laser in lasers){
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }

    }
}
