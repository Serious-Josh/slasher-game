using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_INIT : MonoBehaviour
{

    private PSXEffects psx;

    void Start()
    {
        psx = GetComponent<PSXEffects>();
        psx.affineMapping = false;
        psx.UpdateProperties();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
