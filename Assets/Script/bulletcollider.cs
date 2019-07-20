using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// This class checks the collision of the bullet with the shootable layer and then sends the  amount to health.cs file.
/// </summary>
public class bulletcollider : NetworkBehaviour {

  /// <summary>
  /// This function works  when there is collision with the shootable layer and calls the TakeDamage function.
  /// </summary>
  /// <param name="collision"></param>
	public void OnCollisionEnter(Collision collision)
	{
   
		GameObject hit = collision.transform.parent.gameObject;
		health health = hit.GetComponent<health>();
    if ( health != null )
    {
      health.TakeDamage( 10 );
    }
  }
  
}

