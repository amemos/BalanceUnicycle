using amemo.balanceUnicycle.gameElements;
using amemo.balanceUnicycle.Globals;
using amemo.balanceUnicycle.structurals;
using amemo.balanceUnicycle.structurals.events;
using amemo.balanceUnicycle.structurals.pooler;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace amemo.balanceUnicycle.structurals
{
    /// <summary>
    ///  StackManager handles collecting object moves, stacked objects, stick and balance management.
    ///  Balance state is notified to subscribed objects at certain period.
    ///     
    ///  created by: Ahmet Þentürk
    /// </summary>
    /// 
    [DefaultExecutionOrder(-1)]
    public class StackManager : MonoBehaviour
    {

        [SerializeField]
        private int failDegree = 65;
        [SerializeField]
        private float unbalancingSpeed = 2.0f;

        public CharacterParent characterParent;
        public Character player;

        private TextMeshPro leftStackTMPro;
        private TextMeshPro rightStackTMPro;

        private LevelObject activeCollectableStack;
        private List<Transform> activeCollectables;

        private float unbalanceDegree = 0;
        private int leftStackIndex = 0;
        private int rightStackIndex = 0;

        private void Awake()
        {
            GameManager.Instance.StackManager = this;

            leftStackTMPro = characterParent.leftStackTMPro;
            rightStackTMPro = characterParent.rightStackTMPro;
        }

        private void OnEnable()
        {
            EventManager.onCollectableStackTrigger      += OnCollectableStackTrigger;
            EventManager.onSwipe                        += OnSwipe;
            EventManager.onLevelCompleted               += () => enabled = false;
            EventManager.onLevelFailed                  += () => enabled = false;
        }

        private void OnDisable()
        {
            EventManager.onCollectableStackTrigger      -= OnCollectableStackTrigger;
            EventManager.onSwipe                        -= OnSwipe;
            EventManager.onLevelCompleted               -= () => enabled = false;
            EventManager.onLevelFailed                  -= () => enabled = false;
        }


        private void OnSwipe(Vector2 deltaDir)
        {
            activeCollectableStack.GetComponent<Collectable>().DisableTMPro();
            activeCollectableStack.GetComponent<Collectable>().IsCollected = true;

            int stackCount = activeCollectableStack.GetComponent<Collectable>().GetStackCount();
            if (stackCount < 0)
            {
                RemovePizzaBoxes(stackCount, deltaDir);
                return;
            }

            if (deltaDir.x > 0)
            {
                //Right
                MoveStack(activeCollectables, characterParent.rightStack[0].position + Vector3.forward * 2);
                rightStackIndex += stackCount;
                if (rightStackIndex < 0) rightStackIndex = 0;
                EnablePizzaBoxes(characterParent.rightStack, rightStackIndex);
            }
            else
            {
                //Left
                MoveStack(activeCollectables, characterParent.leftStack[0].position + Vector3.forward * 2);
                leftStackIndex += stackCount;
                if (leftStackIndex < 0) leftStackIndex = 0;
                EnablePizzaBoxes(characterParent.leftStack, leftStackIndex);
            }

            rightStackTMPro.text = rightStackIndex.ToString();
            leftStackTMPro.text = leftStackIndex.ToString();

        }

        private void RemovePizzaBoxes(int stackCount, Vector2 deltaDir)
        {
            int count = Mathf.Abs(stackCount);
            if (deltaDir.x > 0)
            {
                //Right
                for (int i = 0; i < count; i++)
                {
                    if (rightStackIndex == 0)
                        break;
                    Transform pizzaBox = ObjectPooler.Instance.GetPooledObject(ObjectType.E_PIZZA_BOX).transform;
                    pizzaBox.transform.position = characterParent.rightStack[0].position;
                    pizzaBox.gameObject.SetActive(true);
                    pizzaBox.DOMoveY(-6, 0.65f).SetDelay(i * 0.02f).OnComplete(() =>
                    {
                        ObjectPooler.Instance.DestroyGameObj(pizzaBox.gameObject);
                    });
                    characterParent.rightStack[rightStackIndex - 1].GetComponent<MeshRenderer>().enabled = false;
                    rightStackIndex--;

                }
            }
            else
            {
                //Left
                for (int i = 0; i < count; i++)
                {
                    if (leftStackIndex == 0)
                        break;
                    Transform pizzaBox = ObjectPooler.Instance.GetPooledObject(ObjectType.E_PIZZA_BOX).transform;
                    pizzaBox.transform.position = characterParent.leftStack[0].position;
                    pizzaBox.gameObject.SetActive(true);
                    pizzaBox.DOMoveY(-6, 0.65f).SetDelay(i * 0.02f).OnComplete(() =>
                    {
                        ObjectPooler.Instance.DestroyGameObj(pizzaBox.gameObject);
                    });
                    characterParent.leftStack[leftStackIndex - 1].GetComponent<MeshRenderer>().enabled = false;
                    leftStackIndex--;

                }

            }
            rightStackTMPro.text = rightStackIndex.ToString();
            leftStackTMPro.text = leftStackIndex.ToString();
            DOVirtual.DelayedCall(0.3f, () => unbalanceDegree = CalculateUnBalanceIntensity());
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
                item.DOMove(refPos /*+ Vector3.up * 0.1f * index*/, 0.2f + 0.03f * index).OnComplete(() =>
                {
                    ObjectPooler.Instance.DestroyGameObj(item.gameObject);
                });
                index++;
            }
            DOVirtual.DelayedCall(0.2f + index * 0.03f, () => unbalanceDegree = CalculateUnBalanceIntensity());
        }


        private void EnablePizzaBoxes(List<Transform> stack, int activeCount)
        {
            int activeIndex = 0;
            for (int i = 0; i < stack.Count; i++)
            {
                MeshRenderer meshRenderer = stack[i].GetComponent<MeshRenderer>();

                if (i < activeCount)
                {
                    DOVirtual.DelayedCall(activeIndex * 0.07f, () => {
                        meshRenderer.enabled = true;
                    });

                    if (!meshRenderer.enabled)
                        activeIndex++;
                }
                else
                    meshRenderer.enabled = false;

            }
        }

        private int CalculateUnBalanceIntensity()
        {
            if (unbalanceDegree > 0)
            {
                if (leftStackIndex - rightStackIndex > 0)
                    return (int)unbalanceDegree + 1;
            }
            else if (unbalanceDegree < 0)
            {
                if (leftStackIndex - rightStackIndex < 0)
                    return leftStackIndex - rightStackIndex;
            }

            return leftStackIndex - rightStackIndex;
        }


        private void Update()
        {
            CalculateSlope();

            if (Time.frameCount % 10 == 0)
            {
                GiveSomeSlope();
                characterParent.stick.DORotate(Vector3.forward * unbalanceDegree, 0.4f);
            }

        }

        private void GiveSomeSlope()
        {
            Debug.Log(unbalanceDegree);

            EventManager.UpdateIndicator(unbalanceDegree);

            if (Mathf.Abs(unbalanceDegree) > failDegree)
            {
                EventManager.LevelFailed();
                gameObject.SetActive(false);
            }
        }

        private void CalculateSlope()
        {
            if (unbalanceDegree == 0)
                return;
            else if (unbalanceDegree > 0)
                unbalanceDegree += Time.deltaTime * unbalancingSpeed;
            else if (unbalanceDegree < 0)
                unbalanceDegree -= Time.deltaTime * unbalancingSpeed;
        }
    }
}

