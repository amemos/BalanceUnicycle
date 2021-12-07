using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> leftStack;
    [SerializeField]
    private List<Transform> rightStack;


    private void OnEnable()
    {
        
    }


    private void OnDisable()
    {
        
    }

    private int GetUnBalanceIntensity()
    {
        return leftStack.Count - rightStack.Count;
    }


    private void Update()
    {
        if(Time.frameCount % 10 == 0)
        {
            GivesomeSlope();
        }    
    }

    private void GivesomeSlope()
    {
        for (int i = 0; i < leftStack.Count; i++)
        {
            leftStack[i].DORotate(Vector3.forward * 0.3f * i, 0.2f);
            rightStack[i].DORotate(Vector3.forward * 0.3f * i, 0.2f);
        }
    }
}
