using System.Collections;
using System.Collections.Generic;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;

public class MeleeAttackRangeYSetter : MonoBehaviour
{
    private BehaviourTreeOwner behaviourTree;
    
    void Start()
    {
        behaviourTree = GetComponentInParent<BehaviourTreeOwner>();
    }

    void Update()
    {
        transform.localPosition =
            new Vector2(0, (1 / 0.7f) * behaviourTree.blackboard.GetVariable<float>("Attack Range").value);
    }
}
