using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Trigger : MonoBehaviour {

    public Camera mainCam;
    public Camera secCam;
    
    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        secCam.enabled = true;
        mainCam.enabled = false;
    }

}