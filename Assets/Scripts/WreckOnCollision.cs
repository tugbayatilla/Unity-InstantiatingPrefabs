using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WreckOnCollision : MonoBehaviour
{
    public GameObject wreckedVersion;

    // Update is called once per frame
    void OnCollisionEnter()
    {
		Debug.Log("OnCollisionEnter");

        Destroy(gameObject);
		if (wreckedVersion) { 
			Instantiate(wreckedVersion,transform.position,transform.rotation);
		}
    }
}
