using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUIElements : MonoBehaviour
{
    public float hoverScaleIncrease = 0.1f; // 鼠标悬停时的缩放增加量
    private Vector3 originalScale;

    private bool isDragging = false;

    public KeyCode keyCode;


    protected virtual void Start()
    {
        originalScale = transform.localScale;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            OnMouseOver();
            OnMouseDown();
        }
        if(Input.GetKey(keyCode)) MouseDragAction();
        if(Input.GetKeyUp(keyCode)) OnMouseUp();
    }

    protected virtual void OnMouseOver()
    {
       if (Cursor.visible) transform.localScale = originalScale * (1 + hoverScaleIncrease);
    }

    protected virtual void OnMouseExit()
    {
        if (Cursor.visible) transform.localScale = originalScale;
    }

    protected virtual void OnMouseDown()
    {
        Cursor.visible = false;
    }

    protected virtual void OnMouseUp()
    {
        if (!Cursor.visible) transform.localScale = originalScale;

        Cursor.visible = true;
    }

    private void OnMouseDrag()
    {
        MouseDragAction();
    }

    protected virtual void MouseDragAction()
    {

    }
}
