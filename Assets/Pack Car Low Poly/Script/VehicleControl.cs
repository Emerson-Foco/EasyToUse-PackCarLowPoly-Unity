using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class VehicleControl : MonoBehaviour
{
    //Verification:
    public bool playerOn = false;
    public bool handBrakeActive = false;

    //Camera:
    public Camera playerCam;

    //Whells
    public WheelCollider wheel_FR, wheel_FL;
    public WheelCollider wheel_BR, wheel_BL;
    
    //Vehicle settings
    public float motorForce;
    public float brakForce;
    public float wheelCurve;
    
    //Controller:
    float v;
    float h;

    // Update is called once per frame
    void Update()
    {
        //Player On:
        if (playerOn)
        {
            
            //Keyboard:
            v = Input.GetAxis("Vertical") * motorForce;
            h = Input.GetAxis("Horizontal") * wheelCurve;

            //Acceleration:
            wheel_BR.motorTorque = v * (1);
            wheel_BL.motorTorque = v * (1);
            
            //Curve:
            wheel_FR.steerAngle = h;
            wheel_FL.steerAngle = h;

            //Brake:
            if(Input.GetKeyDown(KeyCode.Space))
            {
                wheel_BR.brakeTorque = 0;
                wheel_BL.brakeTorque = 0;
                handBrakeActive = false;
            }

            if(Input.GetKey(KeyCode.Space))
            {
                wheel_BR.brakeTorque = brakForce * 100;
                wheel_BL.brakeTorque = brakForce * 100;
                handBrakeActive = true;
            }
            else if (Input.GetAxis("Vertical") != 0)
            {
                wheel_BR.brakeTorque = 0;
                wheel_BL.brakeTorque = 0;
                handBrakeActive = false;
            }
            else
            {
                wheel_BR.brakeTorque = brakForce * 25;
                wheel_BL.brakeTorque = brakForce * 25;
                wheel_BR.motorTorque = 0;
                wheel_BL.motorTorque = 0;
            }

            //Camera:
            if (playerCam.gameObject.activeSelf)
            {
            }
            else
            {
                playerCam.gameObject.SetActive(true);
            }
        }
        
        //Player Off:
        else
        {
            //Camera:
            playerCam.gameObject.SetActive(false);
            
            //Brake:
            if(handBrakeActive)
            {
                wheel_BR.brakeTorque = brakForce * 100;
                wheel_BL.brakeTorque = brakForce * 100;
            }
            else
            {
                wheel_BR.brakeTorque = 0;
                wheel_BL.brakeTorque = 0;
            }

        }
    }
}
