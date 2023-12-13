using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VectorMoveView : MonoBehaviour
{
    private LineRenderer _lr;

    private void OnEnable()
    {
        _lr = GetComponent<LineRenderer>();
        //ResetPos();
    }

    public void ResetPos()
    {
        LinePoints(1);
        //AddPoint(0, this.transform.position);
    }

    public void LinePoints(int number)
    {
        _lr.positionCount = number;
        //for (int i = 0; i < _lr.positionCount; i++)
        //{
        //    _lr.SetPosition(0, Vector3.zero);
        //}
    }

    public void AddPoint(int number, Vector3 position)
    { 
        _lr.SetPosition(number, position);
    }
}
