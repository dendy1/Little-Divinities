using UnityEngine;
using UnityEngine.EventSystems;

public class CameraTargetController : MonoBehaviour, IPointerDownHandler
{
    //private CameraController _cameraController;
    
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    private void Start()
    {
        //_cameraController = Camera.main.GetComponent<CameraController>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        clicked++;
        if (clicked == 1) clicktime = Time.time;
 
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            Debug.Log("Double CLick: "+this.GetComponent<RectTransform>().name);
            //_cameraController.CameraTarget = transform;

        }
        else if (clicked > 2 || Time.time - clicktime > 1) 
            clicked = 0;
 
    }
}
