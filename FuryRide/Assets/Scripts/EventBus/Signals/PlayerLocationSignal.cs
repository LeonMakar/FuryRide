using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocationSignal
{
    public Vector3 PlayerPosition { get; private set; }
    public PlayerLocationSignal(Vector3 position )
    {
        PlayerPosition = position;
    }

}
