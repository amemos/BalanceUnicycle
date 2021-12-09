using amemo.balanceUnicycle.gameElements;
using amemo.balanceUnicycle.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParent : LevelObject
{
    [SerializeField]
    private float speed = 1.0f;

    public List<Transform> rightStack;
    public List<Transform> leftStack;

    private bool levelStarted = false;

    protected override void Awake()
    {
        GameManager.Instance.CharacterParent = this;
    }

    private void OnEnable()
    {
        EventManager.onLevelStarted += x => levelStarted = true;
        EventManager.onLevelFailed += () => levelStarted = false;
        EventManager.onLevelCompleted += () => levelStarted = false;
    }

    private void OnDisable()
    {
        EventManager.onLevelStarted -= x => levelStarted = true;
        EventManager.onLevelFailed -= () => levelStarted = false;
        EventManager.onLevelCompleted -= () => levelStarted = false;
    }

    private void Update()
    {
        if(levelStarted) transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    public override void Init()
    {
        SetInitialPosition(transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out LevelEnd levelEnd))
        {
            EventManager.LevelCompleted();
        }
    }


}
