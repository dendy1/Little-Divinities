using UnityEngine;

public class RotateCollider : MonoBehaviour {

    public float TorRotSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Quaternion rotation = Quaternion.AngleAxis(TorRotSpeed * Time.deltaTime, Vector3.down);
        Quaternion rotat = Quaternion.AngleAxis(TorRotSpeed * Time.deltaTime, Vector3.down);

        transform.rotation *= rotation;
        transform.rotation *= rotat;
    }
}
