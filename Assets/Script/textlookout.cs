using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// This class take care of the player name to be represented over the player 
/// By default it will show the network Id set by the network identity manager , but you have an option to change the name from GUI interface 
/// and that will be displayed over the network
/// </summary>
public class textlookout : NetworkBehaviour
{
    public NetworkInstanceId PlayerId;/*! Stores the played ID , which is set be the network identity manager */
    [SyncVar]
    public string username;/*! Stores the player ID in the toString() type */
    public GameObject ShowID;/*! The game object of a 3D text is added in here from unity editior*/
    /// <summary>
    /// Shows the GUI for the changing name 
    /// </summary>
    void OnGUI()
    {
        PlayerId = GetComponent<NetworkIdentity>().netId;
        //username = PlayerId.ToString(); // to check 
        if (isLocalPlayer)
        {
            username = GUI.TextField(new Rect(27, Screen.height - 38, 97, 33), username);
            if (GUI.Button(new Rect(126, Screen.height - 38, 97, 33), "Change"))
            {
                CmdChange(username);
            }
        }

    }
    /// <summary>
    /// When the player changes his/her name in GUI , and clicks on the change button , this command will be called
    /// and the name will be updated over the network.
    /// </summary>
    /// <param name="tempname"></param>
    [Command]
    public void CmdChange(string tempname)
    {
        username = tempname;
        ShowID.GetComponent<TextMesh>().text = username;
    }


    /// <summary>
    /// By default the start function will load the username as PlayerID.
    /// </summary>
    void Start()
    {
        PlayerId = GetComponent<NetworkIdentity>().netId;
        username = PlayerId.ToString(); // to check 
        //ShowID.GetComponent<TextMesh>().text = username;
    }
    /// <summary>
    /// This update function will be called every frame , and will update the username when changed.
    /// </summary>
    void Update()
    {
        ShowID.GetComponent<TextMesh>().text = username;

    }
}
