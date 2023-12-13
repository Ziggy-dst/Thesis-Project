using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSlider : PanelUIElements
{
    protected override void MouseDragAction()
    {
        float mouseYDelta = Input.GetAxis("Mouse Y") * Time.deltaTime;
        transform.Translate(0, mouseYDelta, 0);

        // 另一个滑块需要以相同速度反方向移动
        // GameObject otherSlider = slider == sliderA ? sliderB : sliderA;
        // otherSlider.transform.Translate(0, -mouseYDelta, 0);
    }
}
