using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : SnakeBody
{
    [Header("Movement Config")]
    [SerializeField]
    private float _intervalMovement = 1f;
    [SerializeField]
    private float _subtractTime = 0.05f;
    private Vector2 _direction;
    private List<SnakeBody> _snakeBody;
    [SerializeField]
    private GameObject _snakePrefab;
    private bool _alive = true;

    private void Awake()
    {
        this._snakeBody = new List<SnakeBody>();
    }

    private void Start()
    {
        this._snakeBody.Add(this);
        Vector2 actualPosition = new Vector2(transform.position.x, transform.position.z);
        this.ActualPosition = actualPosition;
        Invoke(nameof(ProcessSnakePosition), _intervalMovement);
    }

    public void SetDirection(Vector2 direction)
    {
        this._direction = direction;
    }

    private void ProcessSnakePosition()
    {
        if (IsAlive())
        {
            Vector3 nextPosition = this.gameObject.transform.position + new Vector3(this._direction.x, 0, this._direction.y);
            CheckSnakeOutLevel(nextPosition);
            CheckHeadCollisionWithBody(nextPosition);
            CheckCollisionWithObstacle(nextPosition);
            if (IsAlive())
            {
                this.gameObject.transform.position = nextPosition;
                Vector2 actualPosition = new Vector2(transform.position.x, transform.position.z);
                this.ActualPosition = actualPosition;
                UpdateSnakePositionData();
                UpdateBodySnake();
                Invoke(nameof(ProcessSnakePosition), _intervalMovement);
            }
        }
    }

    private void CheckSnakeOutLevel(Vector3 nextPosition)
    {
        GridController gridController = GridController.gridController;
        int wSize = gridController.GetWSize();
        int hSize = gridController.GetHSize();
        if (nextPosition.x < 0 || nextPosition.z < 0 || nextPosition.x > (wSize - 1) || nextPosition.z > (hSize - 1))
        {
            Die();
        }
    }

    private void CheckHeadCollisionWithBody(Vector3 nextPosition)
    {
        if (this._snakeBody.Count > 1)
        {
            for (int i = 1; i < this._snakeBody.Count; i++)
            {
                SnakeBody snakeBody = this._snakeBody[i];
                if (snakeBody.ActualPosition.Equals(new Vector2(nextPosition.x, nextPosition.z)))
                {
                    Die();
                }
            }
        }
    }

    private void CheckCollisionWithObstacle(Vector3 nextPosition)
    {
        bool hasCollision = GridController.gridController.HasObstacleInPosition(nextPosition);
        if (hasCollision)
        {
            Die();
        }
    }

    public void SpeedUp()
    {
        if (this._intervalMovement > 0.1)
        {
            this._intervalMovement -= _subtractTime;
        }
    }

    private void Die()
    {
        if (IsAlive())
        {
            this._alive = false;
            GridController.gridController.SetLoseUIActive();
        }
    }

    public bool IsAlive()
    {
        return this._alive;
    }

    public void IncreaseSnakeSize()
    {
        SnakeBody previousSnakeBody = this._snakeBody[this._snakeBody.Count - 1];
        SnakeBody snakeBody = Instantiate(this._snakePrefab, previousSnakeBody.transform.position, previousSnakeBody.transform.rotation).GetComponent<SnakeBody>();
        Vector2 actualPosition = new Vector2(snakeBody.transform.position.x, snakeBody.transform.position.z);
        snakeBody.ActualPosition = actualPosition;
        this._snakeBody.Add(snakeBody);
    }

    private void UpdateSnakePositionData()
    {
        if (this._snakeBody.Count > 1)
        {
            for (int i = 1; i < this._snakeBody.Count; i++)
            {
                SnakeBody previousSnakeBody = this._snakeBody[i - 1];
                SnakeBody snakeBody = this._snakeBody[i];
                snakeBody.ActualPosition = previousSnakeBody.PreviousPosition;
            }
        }
    }

    private void UpdateBodySnake()
    {
        for (int i = 1; i < this._snakeBody.Count; i++)
        {
            SnakeBody snakeBody = this._snakeBody[i];
            Vector3 position = new Vector3(snakeBody.ActualPosition.x, snakeBody.transform.position.y, snakeBody.ActualPosition.y);
            snakeBody.transform.position = position;
        }
    }
}
