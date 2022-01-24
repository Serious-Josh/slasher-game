using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRoom : MonoBehaviour
{

    public CharacterController controller;


    // Start is called before the first frame update
    void Update()
    {
        Vector3 movement;
        movement = transform.up * 0.001f;
        controller.Move(movement - Vector3.forward * 0.05f);
    }
}