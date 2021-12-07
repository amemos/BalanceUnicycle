using amemo.balanceUnicycle.Globals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterParent : LevelObject
{
    [SerializeField]
    private float speed = 1.0f;


    private void Update() => transform.Translate(Vector3.forward * Time.deltaTime * speed);


    public override void Init()
    {

        SetInitialPosition(transform.position);
    }


}
