using System.Collections.Generic;
using UnityEngine;

public class TornadoGravity : MonoBehaviour {

    public float ForceG;
    public float BodyKoeff;

    private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();
    private Rigidbody componentRigidbody;


    // Use this for initialization
    void Start () {

        componentRigidbody = GetComponent<Rigidbody>();
        }
	
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Add(other.attachedRigidbody);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            affectedBodies.Remove(other.attachedRigidbody);
        }
    }

    // Update is called once per frame
    private void FixedUpdate() {
	
        foreach (Rigidbody body in affectedBodies)
        {
            Vector3 directionToTor = (transform.position - body.position).normalized;

            float distance = (transform.position - body.position).magnitude;
            float strength = ForceG * body.mass * componentRigidbody.mass / (distance * distance);

            body.AddForce(directionToTor * strength * BodyKoeff);

        }
    }
}
