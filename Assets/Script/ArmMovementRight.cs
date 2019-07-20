using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;

/// <summary>
/// It captures the right  controller  and the right leap hand coordinates and places them on the right IK  End effector.
/// Then the IK works fine
/// </summary>
public class ArmMovementRight : NetworkBehaviour
    {
        public GameObject Bone;/*! Taking the right end effector  */

    public GameObject RightInterationHand;/*! This is used to identify the right hand interaction model*/
        /// <summary>
        /// Find the right hand interaction model 
        /// </summary>
    void Start()
        {
            RightInterationHand = GameObject.Find("Interaction Hand (Right)");

        }
    /// <summary>
    /// Returns if not a local player 
    /// Takes the coordinates of the right controller  and places it over the Bone as descibed above 
    /// Also checks, if the right hand interaction model is enabled the Bone that is the end effector gets the coordinates of the right hand Palm from Leap .
    /// </summary>
    void Update()
        {
            if (!isLocalPlayer)
            {
                
                return;
            }

           

        ///End Effector Gets the coordinates of the right Controller 
                Bone.transform.position = Camera.main.transform.parent.GetChild(1).gameObject.transform.position;
                Bone.transform.rotation = Camera.main.transform.parent.GetChild(1).gameObject.transform.rotation;

        ///End Effector Gets the coordinates of the right palm of the leap hand
        if (GameObject.Find("Right Interaction Hand Contact Bones").active== true)
            {
                Debug.Log("Right Interaction Hand Contact Bones Found");
                Bone.transform.position = RightInterationHand.transform.GetChild(0).transform.position;
                Bone.transform.rotation= RightInterationHand.transform.GetChild(0).transform.rotation;
            }


        }
    }
