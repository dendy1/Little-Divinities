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
                if (firstMinion != secondMinion && firstMinion.BaseStats.resourceTypeProduction ==
                    secondMinion.BaseStats.resourceTypeProduction)
                {
                    firstMinion.BaseStats.MergeWith(secondMinion.BaseStats);
                    firstMinion.Refresh();
                    firstMinion.transform.position = Spawner.RandomVector3();
                    secondMinion.dead?.Invoke();
                }
            }
        }
    }
}
