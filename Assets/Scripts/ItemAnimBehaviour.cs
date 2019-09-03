using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Item _item = animator.gameObject.GetComponent<Item>();
        _item.Spawn();
    }
}
