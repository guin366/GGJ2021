using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBox : MonoBehaviour
{
    public bool grabbed;
    public bool thrown;
    public float explosiveForce;
    
    void OnCollisionEnter(Collision other)
    {
        if(grabbed)
        {
            if(!other.collider.CompareTag("Player"))
            {
                if(other.collider.GetComponent<Rigidbody>() != null)
                {
                Debug.Log("explo");
                other.collider.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce,this.GetComponentInParent<Transform>().position,15,2,ForceMode.Force);
                }
            }
        }  
        else if(thrown)
        {
            if(!other.collider.CompareTag("Player"))
            {
                if(other.collider.GetComponent<Rigidbody>() != null)
                {
                Debug.Log("explo");
                other.collider.GetComponent<Rigidbody>().AddExplosionForce(5000,this.GetComponentInParent<Transform>().position,25,2,ForceMode.Force);
                thrown = false;
                }
            }
        }  
    }
}