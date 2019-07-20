using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;
using UnityEngine.XR;
/// <summary>
/// This functionality works on the local player only. It hides the Left IK arms when left leap bone structure is visible.
/// </summary>
public class HandMotionLocalLeapLeft : NetworkBehaviour
{
    public GameObject LeftArmBicep; /*! This Game object takes the game object in unity editor representing Left Upper Arm */
    public GameObject LeftArmElbow; /*! This Game object takes the game object in unity editor representing Left Lower Arm */


    /// <summary>
    /// Update is called once per frame.
    /// It hides the Left IK arms when left leap bone structure is visible.
    /// </summary>
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        LeftArmBicep.active = true;
        LeftArmElbow.active = true;

       
       ///hiding the left arm 
        if (GameObject.Find("Left Interaction Hand Contact Bones").active == true)
        {

            LeftArmBicep.active = false;
            LeftArmElbow.active = false;


        }






    }
}
