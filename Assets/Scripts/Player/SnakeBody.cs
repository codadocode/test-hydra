using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private Vector2 _actualPosition;
    private Vector2 _previousPosition;

    public Vector2 ActualPosition
    {
        get { return this._actualPosition; }
        set
        {
            _previousPosition = _actualPosition;
            _actualPosition = value;
        }
    }

    public Vector2 PreviousPosition
    {
        get { return this._previousPosition; }
    }
}
