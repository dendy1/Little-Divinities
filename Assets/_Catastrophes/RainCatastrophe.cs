using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCatastrophe : AbstractCatastrophe
{
   [SerializeField] private List<BaseScriptableEffect> effects;
   
   public override void EffectOnMinions(List<MinionEffectable> minions)
   {
      StartCoroutine(EffectCoroutine(minions));
   }

   private IEnumerator EffectCoroutine(List<MinionEffectable> minions)
   {
      while (Timer < duration)
      {
         foreach (var minion in minions)
         {
            foreach (var effect in effects)
            {
               minion.AddEffect(effect.InitializeEffect(minion.MinionStats));
            }
         }

         Timer += Time.deltaTime;
         yield return null;
      }
      
      gameObject.SetActive(false);
   }
}