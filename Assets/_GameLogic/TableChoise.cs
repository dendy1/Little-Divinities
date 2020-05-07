using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableChoise : MonoBehaviour
{

    public void OnEnter()
    {
        Image _This = this.GetComponent<Image>();
        _This.color = new Color(186, 0, 186);
    }

    public void OnExit()
    {
        Image _This = this.GetComponent<Image>();
        _This.color = new Color(255, 255, 255);
    }
}
