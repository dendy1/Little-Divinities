using UnityEngine;
using UnityEngine.UI;

public class TableChoise : MonoBehaviour
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void OnEnter()
    {
        _image.color = new Color(186, 0, 186);
    }

    public void OnExit()
    {
        _image.color = new Color(255, 255, 255);
    }
}
