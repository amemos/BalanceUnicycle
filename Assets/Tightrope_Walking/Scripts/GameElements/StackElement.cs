using amemo.balanceUnicycle.Globals;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackElement : LevelObject
{
    [SerializeField]
    private int stackIndex;
    [SerializeField]
    private StackSide stackSide;

    public override void Init()
    {
        objectType = ObjectType.E_PIZZA_BOX;
        SetInitialPosition(transform.position);
    }

    private void OnEnable()
    {
        EventManager.onIndicatorUpdate += UpdateSlope;
    }

    private void OnDisable()
    {
        EventManager.onIndicatorUpdate += UpdateSlope;
    }

    bool deviation;
    private void UpdateSlope(float degree)
    {
        float step = -degree / 16;

        float angle = (stackIndex * step);
        if (Mathf.Abs(angle) > Mathf.Abs(degree))
        {
            angle = -degree;
            //deviation = true;
        }
        transform.DOLocalRotate(Vector3.forward * angle, 0.2f);

    }

    //private void LateUpdate()
    //{
    //    if(deviation)
    //        transform.DOMoveX(3.5f, 0.2f);
    //}

}
