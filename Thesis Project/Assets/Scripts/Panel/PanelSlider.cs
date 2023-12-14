using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSlider : PanelUIElements
{
    [HideInInspector] public bool isDragging = false;
    public float stopDistance = 0.1f;
    public float maxY = 3.5f;
    public float minY = -1.5f;

    [HideInInspector] public float percentage = 0;

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
