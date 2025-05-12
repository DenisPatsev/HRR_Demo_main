using System;
using UnityEngine;

[Serializable]
public class BodyPart : IBodyPart
{
    public GameObject part;
    public Vector3 partRotation;
    
    public void SetPartRotation()
    {
        part.transform.localRotation = Quaternion.Euler(partRotation);
    }
}