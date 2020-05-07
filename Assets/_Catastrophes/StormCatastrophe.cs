using UnityEngine;

public class StormCatastrophe : AbstractCatastrophe
{
    private void Update()
    {
        if (!IsEnabled)
            return;
      
        if (Timer.IsReady)
        {
            foreach (var minion in islandController.MinionsEffectable)
            {
                foreach (var effect in effects)
                {
                    minion.AddEffect(effect.InitializeEffect(minion.MinionStats));
                }
            }
            
            IsEnabled = false;
            islandController.catastropheEnabled = false;
            gameObject.SetActive(false);
        }
    }
}
