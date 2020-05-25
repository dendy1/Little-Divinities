using UnityEngine;

public class MergeIslandController : IslandController
{
    private void OnMouseDown()
    {
        if (TranslationManager.Instance.SelectedMinion)
        {  
            var ray = MainCamera.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Selected.RemoveFromIsland();
                Selected.StateMachine.ChangeState(Selected.mergeState);
                Selected.transform.position = hit.point;
                AddMinion(Selected);
                Selected = null;
            
                GameManager.Instance.SetActiveIslandsOutline(false);

                if (MinionsCount > 1)
                {
                    MergeMinions();
                }
            }
        }
    }

    private void MergeMinions()
    {
        foreach (var firstMinion in Minions)
        {
            foreach (var secondMinion in Minions)
            {
                if (!firstMinion.Merged && !secondMinion.Merged && firstMinion != secondMinion && firstMinion.BaseStats.resourceTypeProduction ==
                    secondMinion.BaseStats.resourceTypeProduction)
                {
                    firstMinion.MergeWith(secondMinion.BaseStats);
                    firstMinion.Merged = true;
                    var transform1 = firstMinion.transform;
                    transform1.position = (transform1.position + secondMinion.transform.position) / 2;
                    secondMinion.dead?.Invoke();
                }
            }
        }
    }
}
