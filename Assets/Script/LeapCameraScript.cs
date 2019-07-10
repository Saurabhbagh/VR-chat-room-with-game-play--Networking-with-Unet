using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class was never used
/// </summary>
public class LeapCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.FindGameObjectWithTag("leapcamera").transform.position=Camera.main.transform.position;
        GameObject.FindGameObjectWithTag("leapcamera").transform.rotation = Camera.main.transform.rotation;
    }
}
