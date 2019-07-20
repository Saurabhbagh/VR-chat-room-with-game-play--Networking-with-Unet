using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Valve.VR;
using UnityEngine.XR;

/// <summary>
/// This class takes care of the player movement and shooting of bullets 
/// </summary>

public class PlayerController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    private Vector3 offset;/*! This offset is used to make sure that players are getting spawned at diffrent places */

    /// <summary>
    /// In every frame the Update function is called. It also identify the type of headset and also if it local player or not. 
    /// This function is also responsible for the movement of the player and also takes care of the left and the right triggers of the controller .
    /// The left and right arms are disabled when the leap vr detects ours hands
    /// </summary>

    void Update()

    {

        Debug.Log(XRDevice.model);/// detects the VR headset 

        if (XRDevice.model == "Vive MV")
        {

            Debug.Log(XRDevice.model + "inside vive ");
            if (!isLocalPlayer) /// checks if it is a local player or not 
            {
                return; ///returns when not a local player
            }



            this.transform.position = Camera.main.transform.position;/// takes the camera position and sets it for the player 
            this.transform.rotation = Camera.main.transform.rotation;/// takes the camera rotation and sets it for the player 


            /// if statement below checks  if the left triger of the controller is enabled or not 
            if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                Debug.Log("trigger pressed left");
                bulletSpawn.transform.position = GameObject.FindGameObjectWithTag("controllerleft").transform.position;/// this is copying the position of the left controller 
                bulletSpawn.transform.rotation = GameObject.FindGameObjectWithTag("controllerleft").transform.rotation;/// this is copying the rotation of the left controller
                CmdFireLeft(bulletSpawn.transform.position, bulletSpawn.transform.rotation);/// invokes the Command fire left to fire it from the postion being sent


            }
            /// if statement below checks  if the right triger of the controller is enabled or not
            if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                Debug.Log("trigger pressed Right");
                bulletSpawn.transform.position = GameObject.FindGameObjectWithTag("controllerright").transform.position;/// this is copying the position of the right controller
                bulletSpawn.transform.rotation = GameObject.FindGameObjectWithTag("controllerright").transform.rotation;/// this is copying the rotation of the right controller
                CmdFireRight(bulletSpawn.transform.position, bulletSpawn.transform.rotation);/// invokes the Command fire right to fire it from the postion being sent
            }

        }
        /// Does the same thing as the if statement above , but for other  devices
        else
        {

            Debug.Log(XRDevice.model + "Is the device");
            if (!isLocalPlayer)
            {
                return;
            }

            this.transform.position = Camera.main.transform.position;
            this.transform.rotation = Camera.main.transform.rotation;

            if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                Debug.Log("trigger pressed left");
                bulletSpawn.transform.position = GameObject.FindGameObjectWithTag("controllerleft").transform.position;
                bulletSpawn.transform.rotation = GameObject.FindGameObjectWithTag("controllerleft").transform.rotation;
                CmdFireLeft(bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            }

            if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                Debug.Log("trigger pressed Right");
                bulletSpawn.transform.position = GameObject.FindGameObjectWithTag("controllerright").transform.position;
                bulletSpawn.transform.rotation = GameObject.FindGameObjectWithTag("controllerright").transform.rotation;
                CmdFireRight(bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            }
        }

    }


    /// <summary>
    /// Command :This [Command] code is called on the Client ,but it is run on the Server!
    /// This is invoke when Left trigger is pressed with parameter x is being position of controller and parameter y is rotation of the controller.
    /// </summary>
    /// <param name="x">position of the left controller</param>
    /// <param name="y">rotation of the left controller</param>

    [Command]
    public void CmdFireLeft(Vector3 x, Quaternion y)
    {
        
        /// Create the Bullet from the Bullet Prefab
        
        var bullet = (GameObject)Instantiate(bulletPrefab, x, y);
        /// Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
        /// Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);
        /// Destroy the bullet after 8 seconds
        Destroy(bullet, 8.0f);


    }

    /// <summary>
    /// This is invoke when Right trigger is pressed with parameter x is being position of controller and parameter y is rotation of the controller.
    /// </summary>
    /// <param name="x">position of the right controller</param>
    /// <param name="y">rotation of the right controller</param>

    [Command]
    public void CmdFireRight(Vector3 x, Quaternion y)
    {
        Debug.Log("Bullet pressed Right");
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(bulletPrefab, x, y);
        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 8 seconds
        Destroy(bullet, 8.0f);


    }

    /// <summary>
    /// Setting offset of the player and initializing the player position with offset. 
    /// </summary>
    /// 
    public override void OnStartLocalPlayer()
    {
        offset = new Vector3(Random.Range(2, 10), 0, Random.Range(2, 10));

        this.transform.position = Camera.main.transform.position + offset;



    }

}