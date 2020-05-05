using UnityEngine;

public class TornadoConstraint : MonoBehaviour {

    public Transform TargetTor;
	
	// Update is called once per frame
	private void FixedUpdate () 
    {

        Quaternion rotation = Quaternion.FromToRotation(-transform.up, TargetTor.position - transform.position);
        transform.rotation = rotation * transform.rotation;

	}
}
