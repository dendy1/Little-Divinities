public class VacationIslandController : IslandController
{
    private void OnMouseDown()
    {
        if (TranslationManager.Instance.SelectedMinion)
        {
            Selected.RemoveFromIsland();
            Selected.StateMachine.ChangeState(Selected.vacationState);
            var pos = Spawner.RandomVector3();
            Selected.transform.position = pos;
            AddMinion(Selected);
            Selected = null;
            
            GameManager.Instance.SetActiveIslandsOutline(false);
        }
    }
}
