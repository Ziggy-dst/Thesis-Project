using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Framework;
using NodeCanvas.Tasks.Actions;
using ParadoxNotion.Design;
using UnityEngine;

[Description("Check the nearest selected type of object the agent is facing to")]
public class CheckNearestObstacle : ActionTask
{
    private Vector2 moveDirection;
    private float moveDistance;

    private Blackboard currentBlackboard;

    protected override string OnInit()
    {
        currentBlackboard = agent.GetComponent<Blackboard>();
        return base.OnInit();
    }

    protected override void OnExecute()
    {
        moveDirection = currentBlackboard.GetVariableValue<Vector2>("moveDirection");
        // CheckObstacle();
    }

    protected override void OnUpdate()
    {
        CheckObstacle();
    }

    private void CheckObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(agent.transform.position, moveDirection, Mathf.Infinity);

        // print(hit.collider);

        if (hit.collider != null)
        {
            // 如果检测到障碍物，计算到障碍物的距离
            moveDistance = hit.distance;
            Debug.Log("------------------Raycast--------------------");
            Debug.Log("col: " + hit.collider);
            Debug.Log("hit position: " + hit.point);
            Vector2 slidingTarget = (Vector2)agent.transform.position + (moveDirection * moveDistance);
            currentBlackboard.SetVariableValue("slidingTarget", slidingTarget);
            Debug.Log("ray distance: " + moveDistance);
            // Debug.Log("slid target: " + slidingTarget);
            // EndAction(true);
        }

        Debug.DrawRay(agent.transform.position, moveDirection * moveDistance, Color.red);

        EndAction(true);
    }
}
