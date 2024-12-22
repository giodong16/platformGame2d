using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] bool isTurnOn = false;
    public bool IsTurnOn { get => isTurnOn; set => isTurnOn = value; }
    
}
