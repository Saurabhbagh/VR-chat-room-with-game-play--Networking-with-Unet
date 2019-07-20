using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Valve.VR;
using UnityEngine.XR;

/// <summary>
/// Voice Check works in a walky talky manner. You press the grip button and it  will transfer your voice to other player / clients in the network. You will not be able hear your own voice.
/// Here you convert the voice to float and then to byte to send it on the network. Once the  other non local player who receives it will do the reverse of it .
/// There it will get converted from byte to float and then to Audio Clip
/// </summary>

public class VoiceChecker : NetworkBehaviour
{

    int Checkpoint;
    AudioClip voice;


    void Start()
    {
        if (isLocalPlayer)
        {
            Debug.Log("Checkpoint on start " + Checkpoint);

            foreach (var device in Microphone.devices)
            {
                Debug.Log("Name: " + device);
            }
            if (Application.HasUserAuthorization(UserAuthorization.Microphone))
            {
                Debug.Log("Microphone found with authorization ");

            }
            else
            {
                Debug.Log("Microphone not found");

            }


            voice = Microphone.Start(null, true, 50, 8000); // it takes  lenght and frquency 
          
            while (Microphone.GetPosition(null) < 0) //latency 
            { }

            
        }
    }
    /// <summary>
    /// The fixed update sends your voice over the network by converting the voice to float and then to byte 
    /// </summary>
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            ///for GRIP             
            if (SteamVR_Actions._default.GrabGrip.GetState(SteamVR_Input_Sources.Any))  // for any state it works !!
            {
                
                int currentposition = Microphone.GetPosition(null);
                int recordlength = currentposition - Checkpoint;
               
                if (recordlength > 0)
                {
                    ///Sample count is determined by the length of the float array 
                    float[] datatofloat = new float[recordlength];
                    voice.GetData(datatofloat, Checkpoint); /// fills the array till the check point fills datatofloat  till checkpoint  
                                                            ///creating a byte array 
                    byte[] bytedata = new byte[datatofloat.Length * 4];
                    int count = 0;
                    foreach (float voice in datatofloat)
                    {
                        byte[] data = System.BitConverter.GetBytes(voice);
                        System.Array.Copy(data, 0, bytedata, count, 4);
                        count += 4;
                    }
                    CmdSendData(bytedata);
                }
                Checkpoint = currentposition;
               
            }

        }
    }

    /// <summary>
    /// This methods is taking the byte voice data and converting into float and then to voice again using Audio.create
    /// </summary>
    /// <param name="bytedata">Byte form of voice </param>
   public void SoundOut(byte[] bytedata)
    {
        if (!isLocalPlayer) // sound comes out in other pc 
        {
            float[] ConvertedFloat = new float[(bytedata.Length) / 4];
            for (int i = 0; i < bytedata.Length; i += 4)
            {

                ConvertedFloat[i / 4] = System.BitConverter.ToSingle(bytedata, i); ///converting to float
            }

            GetComponent<AudioSource>().clip = AudioClip.Create("Output", ConvertedFloat.Length, 1, 8000, true, false);
            GetComponent<AudioSource>().clip.SetData(ConvertedFloat, 0);
            GetComponent<AudioSource>().Play();


        }
    }

    /// <summary>
    /// Send data for comvertion from byte to voice.
    /// </summary>
    /// <param name="bytedata"></param>
    [Command]
    public void CmdSendData(byte[] data)
    {
        
        SoundOut(data);
        RpcSendAudioToClients(data); // to send data from server to client 
    }
    /// <summary>
    ///  To send data from server to client 
    /// </summary>
    /// <param name="bytedata"></param>
    [ClientRpc]
    public void RpcSendAudioToClients(byte[] bytedata)
    {
        Debug.Log("Sending the voice ");
        SoundOut(bytedata);
    }






}