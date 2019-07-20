using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;
/// <summary>
/// It captures the left controller  and the left leap hand coordinates and places them on the left IK  End effector.
/// Then the IK works fine
/// </summary>

    public class ArmMovementLeft : NetworkBehaviour
    {
        public GameObject Bone; /*! Taking the left end effector  */
        public GameObject LeftInterationHand;/*! This is used to identify the left hand interaction model*/
        /// <summary>
        /// Find the left hand interaction model 
        /// </summary>
        void Start()
        {
           LeftInterationHand = GameObject.Find("Interaction Hand (Left)");

        }
        /// <summary>
        /// Returns if not a local player 
        /// Takes the coordinates of the left controller  and places it over the Bone as descibed above 
        /// Also checks, if the left hand interaction model is enabled the Bone that is the end effector gets the coordinates of the Left hand Palm from Leap .
        /// </summary>
        void Update()
        {
            if (!isLocalPlayer)
            {
               
                return;
            }

            

            ///End Effector Gets the coordinates of the left Controller 
            Bone.transform.position = Camera.main.transform.parent.GetChild(0).gameObject.transform.position;
            Bone.transform.rotation = Camera.main.transform.parent.GetChild(0).gameObject.transform.rotation;

             ///End Effector Gets the coordinates of the left palm of the leap hand 
         if (GameObject.Find("Left Interaction Hand Contact Bones").active == true)
            {
                Debug.Log("Left Interaction Hand Contact Bones Found");
                Bone.transform.position = LeftInterationHand.transform.GetChild(0).transform.position;
                Bone.transform.rotation = LeftInterationHand.transform.GetChild(0).transform.rotation;
            }
                

        }
    }

