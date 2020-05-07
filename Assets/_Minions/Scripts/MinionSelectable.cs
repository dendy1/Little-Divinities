using UnityEngine;

[RequireComponent(typeof(MinionStats))]
public class MinionSelectable : MonoBehaviour
{
    private MinionStats _minionStats;

    private void Awake()
    {
        _minionStats = GetComponent<MinionStats>();
    }

    public void OnMouseDown()
    {
        TranslationManager.Instance.SelectedMinion = _minionStats;
    }
}
