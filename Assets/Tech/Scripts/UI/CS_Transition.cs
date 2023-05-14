using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Transition : MonoBehaviour
{
     [SerializeField] private Animator _animatorDefaite;
     [SerializeField] private Animator _animatorVictoire;


     public void StartTransitionDefaite()
     {
          _animatorDefaite.SetTrigger("EndGame");
     }

     public void StartTransitionVictoire()
     {
          _animatorVictoire.SetTrigger("EndGame");
     }
}
