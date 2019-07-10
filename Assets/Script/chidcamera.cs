using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/// <summary>
/// This class was never used 
/// </summary>
public class chidcamera : MonoBehaviour
{
    public Transform playerTransform;
    public int depth = -20;

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + new Vector3(0, 10, depth);
        }
    }

    public void setTarget(Transform target)
    {
        playerTransform = target;
    }
}
