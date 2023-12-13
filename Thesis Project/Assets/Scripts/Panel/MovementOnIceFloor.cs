using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;
using NodeCanvas.BehaviourTrees;
using Unity.MLAgents.Integrations.Match3;

/// <summary>
/// drag on the character who would walk on ice
/// </summary>
public class MovementOnIceFloor : MonoBehaviour
{
    private Timeline timeline;
    private BehaviourTreeOwner behaviourTreeOwner;

    private Vector2 initialPosition;
    private Vector2 lastPosition;
    private bool hasMoved = false;

    public float iceSpeed = 4f;

    private Vector2 moveDirection;
    private float moveDistance;

    private bool isMovingOnIce = false;

    void Start()
    {
        ResetSetting();
        timeline = GetComponent<Timeline>();
        behaviourTreeOwner = GetComponent<BehaviourTreeOwner>();
    }

    // Update is called once per frame
    void Update()
    {

        if (TerrainManager.TerrainManagerInstance.currentTerrain == TerrainManager.MapTerrain.Ice)
        {
            // check the move direction
            CheckMoveDirection();

            // calculate the nearest obstacle
            CheckNearestObstacle();

            // move the object
            MoveObject();
        }
        else
        {
            behaviourTreeOwner.enabled = true;
        }
    }

    private void ResetSetting()
    {
        initialPosition = transform.position;
        lastPosition = initialPosition;
        hasMoved = false;
        moveDirection = Vector2.zero;
        moveDistance = 0f;
    }

    /// <summary>
    /// do not check direction again after the object starts to slide
    /// </summary>
    private void CheckMoveDirection()
    {
        Vector2 currentPosition = transform.position;

        if (moveDirection != Vector2.zero)
        {
            moveDirection = (currentPosition - lastPosition).normalized;
            Debug.Log("Moving in direction: " + moveDirection);
            hasMoved = true;
        }

        // lastPosition = currentPosition;
        //
        // Vector2 currentPosition = transform.position;
        // if (currentPosition != lastPosition && !hasMoved)
        // {
        //     moveDirection = (currentPosition - initialPosition);
        //     print("currentPosition " + currentPosition);
        //     print("initialPosition " + initialPosition);
        //     Debug.Log("Movement direction: " + moveDirection);
        //      // 确保只记录第一次移动的方向
        // }

        lastPosition = currentPosition;
    }

    /// <summary>
    /// constantly check the obstacle
    /// </summary>
    private void CheckNearestObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, Mathf.Infinity);

        // print(hit.collider);

        if (hit.collider != null)
        {
            // 如果检测到障碍物，计算到障碍物的距离
            moveDistance = hit.distance;
        }

        Debug.DrawLine(transform.position, hit.point, Color.magenta);

    }

    private void MoveObject()
    {
        // disable behavior tree
        behaviourTreeOwner.enabled = false;

        Vector2 newPosition = (Vector2)transform.position + (moveDirection.normalized * moveDistance);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, iceSpeed * timeline.deltaTime);

        // print(newPosition);
        // print(Vector2.Distance(transform.position, newPosition));

        // if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        // {
        //     ResetSetting();
        //     // enable behavior tree when arrived
        //     behaviourTreeOwner.enabled = true;
        //     hasMoved = false;
        //     // isMovingOnIce = false;
        // }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        ResetSetting();
        // enable behavior tree when arrived
        behaviourTreeOwner.enabled = true;
        hasMoved = false;
    }

}
