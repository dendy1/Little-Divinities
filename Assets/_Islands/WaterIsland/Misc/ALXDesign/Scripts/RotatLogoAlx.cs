using UnityEngine;

public class RotatLogoAlx : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}

    public float RotationSpeed;
    public string HelloWorldString;

    void Update()
    {
        Quaternion rotation = Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.up);
        Quaternion rotat = Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.right);

        transform.rotation *= rotation;
        transform.rotation *= rotat;
    }
}