using UnityEngine;

public class RainCatastrophe : AbstractCatastrophe
{
   private void Update()
   {
      if (!IsEnabled)
         return;
      
      if (Timer.IsReady)
      {
         IsEnabled = false;
         islandController.catastropheEnabled = false;
         gameObject.SetActive(false);
         return;
      }
      
      foreach (var minion in islandController.MinionsEffectable)
      {
         foreach (var effect in effects)
         {
            minion.AddEffect(effect.InitializeEffect(minion.MinionStats));
         }
      }
   }
}