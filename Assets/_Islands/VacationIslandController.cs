using UnityEngine;

public class VacationIslandController : IslandController
{
    private void OnMouseDown()
    {
        if (TranslationManager.Instance.SelectedMinion)
        {  
            var ray = MainCamera.ScreenPointToRay (Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Selected.RemoveFromIsland();
                Selected.StateMachine.ChangeState(Selected.vacationState);
                Selected.transform.position = hit.point;
                AddMinion(Selected);
                Selected = null;

                GameManager.Instance.SetActiveIslandsOutline(false);
            }
        }
    }
}
