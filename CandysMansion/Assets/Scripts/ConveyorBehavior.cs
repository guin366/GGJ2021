using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBehavior : MonoBehaviour
{
   public GameObject belt;
   public Transform endpoint;
   public float speed;

   void OnTriggerStay(Collider other)
   {
       
       other.attachedRigidbody.velocity = other.attachedRigidbody.velocity + (endpoint.position - other.transform.position)*speed*Time.deltaTime;
   }
}
