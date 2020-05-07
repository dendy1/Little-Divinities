using cakeslice;
using UnityEngine;

public class TranslationManager : MonoBehaviourSingleton<TranslationManager>
{
    private MinionStats _selectedMinion;
    public MinionStats SelectedMinion { 
        get => _selectedMinion;
        set
        {
            SetActiveOutline(false);
            _selectedMinion = value;

            if (_selectedMinion)
            {
                if (_selectedMinion.CurrentIslandController is MergeIslandController)
                {
                    // Minion on merge island, so turning outline on vacation island and minion's base island
                    GameManager.Instance.SetActiveVacationIslandOutline(true);
                    _selectedMinion.MainIslandController.SetActiveOutline(true);
                }
                else  if (_selectedMinion.CurrentIslandController is VacationIslandController)
                {
                    // Minion on vacation island, so turning outline on merge island and minion's base island
                    GameManager.Instance.SetActiveMergeIslandOutline(true);
                    _selectedMinion.MainIslandController.SetActiveOutline(true);
                }
                else
                {
                    // Minion on base island, so turning outline on merge island and vacation island
                    GameManager.Instance.SetActiveMergeIslandOutline(true);
                    GameManager.Instance.SetActiveVacationIslandOutline(true);
                }
            }
            
            SetActiveOutline(true);
        }
    }
    
    public void SetActiveOutline(bool active)
    {
        if (!SelectedMinion) return;
        
        foreach (Transform child in SelectedMinion.transform.GetChild(0).GetChild(0))
        {
            var outline = child.GetComponent<Outline>();
            
            if (outline)
                outline.enabled = active;
        }
    }
}
