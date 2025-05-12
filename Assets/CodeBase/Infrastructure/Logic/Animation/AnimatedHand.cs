using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimatedHand
{
    [Range(0f, 1f)] public float gripMultiplier;
    [SerializeField] private HandFinger[] fingers = new HandFinger[4];
    
    public HandFinger[] Fingers => fingers;
}