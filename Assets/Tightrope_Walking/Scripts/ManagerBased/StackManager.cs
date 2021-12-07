using amemo.balanceUnicycle.gameElements;
using amemo.balanceUnicycle.Globals;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class StackManager : MonoBehaviour
{
 
    public CharacterParent characterParent;

    private LevelObject activeCollectableStack;
    private List<Transform> activeCollectables;
    private int leftStackIndex = 0;
    private int rightStackIndex = 0;

    private void Awake()
    {
        GameManager.Instance.StackManager = this;
        characterParent = FindObjectOfType<CharacterParent>(); //dont forget to fix
    }

    private void OnEnable()
    {
        EventManager.onCollectableStackTrigger  += OnCollectableStackTrigger;
        EventManager.onSwipe                    += OnSwipe;
    }

    private void OnDisable()
    {
        EventManager.onCollectableStackTrigger  -= OnCollectableStackTrigger;
        EventManager.onSwipe                    -= OnSwipe;
    }


    private void OnSwipe(Vector2 deltaDir)
    {
        if(deltaDir.x > 0)
        {
            //Right
            MoveStack(activeCollectables, characterParent.rightStack[0].position + rightStackIndex * Vector3.up * 0.1f);
            rightStackIndex += activeCollectableStack.GetComponent<Collectable>().GetStackCount();
            if (rightStackIndex < 0) rightStackIndex = 0;
            EnablePizzaBoxes(characterParent.rightStack, rightStackIndex);
        }
        else
        {
            //Left
            MoveStack(activeCollectables, characterParent.leftStack[0].position + leftStackIndex * Vector3.up * 0.1f);
            leftStackIndex += activeCollectableStack.GetComponent<Collectable>().GetStackCount();
            if (leftStackIndex < 0) leftStackIndex = 0;
            EnablePizzaBoxes(characterParent.leftStack, leftStackIndex);
        }
    }

    private void OnCollectableStackTrigger(LevelObject collectablelevelObj, bool entered)
    {
        activeCollectableStack = collectablelevelObj;
        activeCollectables = activeCollectableStack.GetComponentsInChildren<Transform>().ToList();
        activeCollectables.RemoveAt(0);
        activeCollectables.RemoveAt(activeCollectables.Count - 1);
    }

    private void MoveStack(List<Transform> stackCollectable, Vector3 refPos)
    {
        int index = 0;
        foreach (var item in stackCollectable)
        {
            item.DOMove(refPos + Vector3.up * 0.1f * index, 0.2f + 0.02f * index).OnComplete(()=> 
            {
                ObjectPooler.SharedInstance.DestroyGameObj(item.gameObject);
            });
            index++;
        }
    }


    private void EnablePizzaBoxes(List<Transform> stack, int activeCount)
    {
        for (int i = 0; i < stack.Count; i++)
        {
            MeshRenderer meshRenderer = stack[i].GetComponent<MeshRenderer>();

            if(i < activeCount)
                meshRenderer.enabled = true;
            else
                meshRenderer.enabled = false;
        }
    }


    private int GetUnBalanceIntensity()
    {
        return leftStackIndex - rightStackIndex;
    }


    private void Update()
    {
        if(Time.frameCount % 10 == 0)
        {
            GiveSomeSlope(GetUnBalanceIntensity());
        }    
    }

    private void GiveSomeSlope(int angleMultiplier)
    {
        characterParent.transform.DORotate(Vector3.forward * angleMultiplier * 2, 0.4f);
        for (int i = 0; i < characterParent.leftStack.Count; i++)
        {
            characterParent.leftStack[i].DORotate(Vector3.forward * 0.2f * i * angleMultiplier, 0.1f).SetDelay(i * 0.02f);
            characterParent.rightStack[i].DORotate(Vector3.forward * 0.2f * i * angleMultiplier, 0.1f).SetDelay(i * 0.02f);
        }
    }
}
