using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;
using UnityEngine.XR;
/// <summary>
/// This functionality works on the local player only. It hides the Right IK arms when right leap bone structure is visible.
/// </summary>
public class HandMotionLocalLeap : NetworkBehaviour
{
    
    public GameObject RightArmBicep;/*! This Game object takes the game object in unity editor representing Right Upper Arm */
    public GameObject RightArmElbow;/*! This Game object takes the game object in unity editor representing Right Lower Arm */
 /// <summary>
 /// Update is called once per frame.
 /// It hides the Right IK arms when right leap bone structure is visible.
 /// </summary>
                        

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        RightArmBicep.active = true;
        RightArmElbow.active = true;
        

        ///hiding the right arm 
        if (GameObject.Find("Right Interaction Hand Contact Bones").active == true)
        {
            RightArmBicep.active = false;
            RightArmElbow.active = false;
           
            
        }       
                  

    }
}
