using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using amemo.balanceUnicycle.Globals;

public class StickTrigger : LevelObject
{
    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out LevelObject obj ))
        {

        }
    }
}
