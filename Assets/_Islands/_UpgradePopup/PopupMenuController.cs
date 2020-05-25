using UnityEngine;
using TMPro;

public class PopupMenuController : MonoBehaviour
{
    public TMP_Text currentMaxResources;
    public TMP_Text nextMaxResources;
    
    public TMP_Text currentMaxMinions;
    public TMP_Text nextMaxMinions;
    
    public TMP_Text minionsUpdateCost;
    public TMP_Text resourcesUpdateCost;

    public void Close()
    {
        gameObject.SetActive(false);
        InterfaceManager.Instance.PopupMenuOpened = false;
    }
}
