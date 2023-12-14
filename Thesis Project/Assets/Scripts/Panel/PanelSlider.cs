using System.Collections;
using System.Collections.Generic;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;

public class PanelSlider : PanelUIElements
{
    public bool isDragging = false;
    public float stopDistance = 0.1f;
    public float maxY = 3.5f;
    public float minY = -1.5f;

    [HideInInspector] public float percentage = 0;
    
    //下面我拉一点，到时候可以删掉
    private BehaviourTreeOwner playerBehaviourTree;
    public bool isControllingRange = false;

    //下面我拉一点，到时候可以删掉
    protected override void Start()
    {
        base.Start();
        playerBehaviourTree = GameObject.FindGameObjectWithTag("Player").GetComponent<BehaviourTreeOwner>();
    }
    
    //下面我拉一点，到时候可以删掉
    protected override void Update()
    {
        base.Update();
        if (isControllingRange) playerBehaviourTree.blackboard.GetVariable<float>("Attack Range").value = 1 + percentage * 3f;
    }

    protected override void MouseDragAction()
    {
        isDragging = true;
        float mouseYDelta = Input.GetAxis("Mouse Y") * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + new Vector3(0, mouseYDelta, 0);
        newPosition.y = Mathf.Clamp(Mathf.Round(newPosition.y * 100f) / 100f, minY, maxY);

        SetSliderPosition(newPosition.y);

    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        isDragging = false;
    }

    public void SetSliderPosition(float newY)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        percentage = (newY - minY) / (maxY - minY);
        // print("percentage " + gameObject.name + ": " + percentage);
    }
    
}
