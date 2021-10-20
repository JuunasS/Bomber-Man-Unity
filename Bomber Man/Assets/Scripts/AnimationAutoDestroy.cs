using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAutoDestroy : StateMachineBehaviour
{
    // Remove animation after it ends.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
    }
}
