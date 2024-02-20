using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleObjects : MonoBehaviour
{
    [SerializeField] protected GameObject ParentObject;

    /// <summary>
    /// Moove Forward current Interactible object
    /// </summary>
    public virtual void Update()
    {
    }
}
