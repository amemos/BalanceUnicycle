using amemo.balanceUnicycle.structurals.events;
using UnityEngine;
using DG.Tweening;

public class BodyTarget : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.onIndicatorUpdate += ChangeTargetPosition;
    }


    private void OnDisable()
    {
        EventManager.onIndicatorUpdate -= ChangeTargetPosition;
    }

    private void ChangeTargetPosition(float degree)
    {
        transform.DOMoveX(-degree / 10, 0.5f);
        transform.DOMoveY(1.31f + Mathf.Abs(degree) / 10, 0.5f);
    }
}
