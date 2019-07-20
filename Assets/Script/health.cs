using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

/// <summary>
/// This health class is resposible for making sure that the health is reduced , when the hits are registered in the bulletcollider.cs
/// Also this script takes care of the exit function 
/// </summary>
public class health : NetworkBehaviour
{
 
  public NetworkInstanceId  PlayerId;/*! This game object stores the player id to identify  whose  health is getting reduced*/
  public const int maxHealth = 100;/*! Sets default max health to 100*/
  int count=0;/*! This is used to count the number of player in the network */
  [SyncVar (hook = "OnChangeHealth")]public int currentHealth = maxHealth;/*! SyncVar is used to to sync the current health , whenever the current health value changes on server, it will updated to the clients, such that clients know there is change in value once the method is called and this method will provide updated value to the health bar   */
    public RectTransform healthbar;/*!This is for the UI update */
    
 
    /// <summary>
    /// Calculates the damage on the player and exits as per functionality
    /// </summary>
    /// <param name="amount">The amount is taken when there is a bullet colliding the shootable layer of the player</param>
  public void TakeDamage( int amount )
  {
    PlayerId = GetComponent<NetworkIdentity>().netId;
    NetworkManager nm = FindObjectOfType<NetworkManager>();
     count = nm.numPlayers;/*! Used to find number of players*/
      Debug.Log( "Number of player in the network update " + count );
    Debug.Log( "taking damage" );

    currentHealth -= amount;

    Debug.Log( "player "+ PlayerId + ", health: " + currentHealth);

        /// <summary>
        /// This code checks for the health when it is less than or equal to zero
        /// Íf the server dies , it does nothing and keeps the connection open and deactivates the server. 
        /// When the client does it disables the client.
        /// </summary>

    if ( currentHealth <= 0 )
    {
      if(isServer)
      {
        if ( count > 1 )
        {
          
          //Destroy( GetComponent<Rigidbody>() );
          this.gameObject.active = false;

        }       
        else
          NetworkManager.singleton.StopClient();

      }
      else if(count==0)
      {
                NetworkManager.singleton.StopHost();
            
      }

     else
      {
          
                //NetworkManager.singleton.StopClient();
                this.gameObject.active = false;

      }
      currentHealth = maxHealth;
    }
    }
  


/// <summary>
/// Changing the UI for the health bar.
/// </summary>
/// <param name="health"></param>



  public void OnChangeHealth( int health )
  {
    healthbar.sizeDelta = new Vector2( health * 2, healthbar.sizeDelta.y );
  }

  
    
}

