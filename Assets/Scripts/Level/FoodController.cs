using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodController : MonoBehaviour
{
    private void Update()
    {
        if (GridController.gridController.GetSnakeController().transform.position.Equals(transform.position))
        {
            GridController.gridController.GetSnakeController().IncreaseSnakeSize();
            GridController.gridController.GetSnakeController().SpeedUp();
            ChangeFoodLocation();
        }
    }

    private void ChangeFoodLocation()
    {
        GameObject[,] gridMap = GridController.gridController.GetGroundArray();
        GridController gridController = GridController.gridController;
        int Xpos = Random.Range(0, gridController.GetWSize());
        int Ypos = Random.Range(0, gridController.GetHSize());
        Vector3 foodNextPosition = new Vector3(Xpos, 1, Ypos);
        while (GridController.gridController.HasObstacleInPosition(foodNextPosition))
        {
            Xpos = Random.Range(0, gridController.GetWSize());
            Ypos = Random.Range(0, gridController.GetHSize());
            foodNextPosition = new Vector3(Xpos, 1, Ypos);
        }
        this.transform.position = foodNextPosition;
    }
}
