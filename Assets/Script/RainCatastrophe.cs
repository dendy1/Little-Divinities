using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCatastrophe : AbstractCatastrophe
{
   public float DurationTime = 2f;

   private float _timer;
   private bool _flag;
   private float _power;

   private List<GameObject> _minions;

   public override void EffectOnMinions(List<GameObject> minions)
   {
      _timer = 0;
      StartCoroutine(EffectCoroutine(minions));
   }

   private IEnumerator EffectCoroutine(List<GameObject> minions)
   {
      while (_timer < DurationTime)
      {
         foreach (var minion in minions)
         {
            var descriptor = minion.GetComponent<MinionDescriptor>();
            descriptor.currentPower = descriptor.Power * 0.5f;
         }

         _timer += Time.deltaTime;
         yield return null;
      }

      foreach (var minion in minions)
      {
         var descriptor = minion.GetComponent<MinionDescriptor>();
         descriptor.currentPower = descriptor.Power;
      }
      
      gameObject.SetActive(false);
   }
}