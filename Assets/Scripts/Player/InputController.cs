using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector2 _direction = Vector2.up;

    private void Start()
    {
        UpdateSnakeDirection();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GridController.gridController.GetSnakeController().IncreaseSnakeSize();
        }
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            this._direction = Vector2.up;
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            this._direction = -Vector2.up;
        }else if (Input.GetKeyDown(KeyCode.A))
        {
            this._direction = -Vector2.right;
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            this._direction = Vector2.right;
        }
        UpdateSnakeDirection();
    }

    private void UpdateSnakeDirection()
    {
        GridController.gridController.GetSnakeController().SetDirection(this._direction);
    }
}
