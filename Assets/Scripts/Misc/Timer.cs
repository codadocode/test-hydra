using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _actualTime = 0;
    [SerializeField]
    private float _targetTime;
    private bool _finished = false;
    private bool _started = false;

    private void Update()
    {
        if (IsStarted())
        {
            if (!IsFinished())
            {
                _actualTime += Time.deltaTime;
                CheckFinished();
            }
        }
    }

    public bool IsFinished()
    {
        return this._finished;
    }

    public bool IsStarted()
    {
        return this._started;
    }

    public void ResetTimer()
    {
        this._actualTime = 0;
        this._finished = false;
    }

    public void StartTimer()
    {
        this._started = true;
    }

    private void CheckFinished()
    {
        if (this._actualTime >= this._targetTime)
        {
            this._finished = true;
        }
    }
}
