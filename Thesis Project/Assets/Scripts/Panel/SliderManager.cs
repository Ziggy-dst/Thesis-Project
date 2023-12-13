using UnityEngine;

public class SliderManager : MonoBehaviour
{
    public GameObject sliderA;
    public GameObject sliderB;
    public float hoverScaleIncrease = 0.1f; // 鼠标悬停时的缩放增加量
    private Vector3 sliderAOriginalScale;
    private Vector3 sliderBOriginalScale;
    private GameObject activeSlider = null; // 当前活动的滑块

    private void Start()
    {
        // 保存原始的缩放比例
        sliderAOriginalScale = sliderA.transform.localScale;
        sliderBOriginalScale = sliderB.transform.localScale;
    }

    private void Update()
    {
        if (!activeSlider)
        {
            CheckForHover(sliderA);
            CheckForHover(sliderB);
        }

        if (activeSlider && Input.GetMouseButtonUp(0))
        {
            // 松开鼠标左键，退出拖拽状态，显示cursor
            activeSlider = null;
            Cursor.visible = true;
        }

        if (activeSlider)
        {
            DragSlider(activeSlider);
        }
    }

    private void CheckForHover(GameObject slider)
    {
        Collider2D collider = slider.GetComponent<Collider2D>();
        if (collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            // 鼠标悬停在滑块上
            if (!activeSlider)
            {
                // 只有在不拖拽的状态下才进行缩放
                slider.transform.localScale = sliderAOriginalScale * (1 + hoverScaleIncrease);
            }

            if (Input.GetMouseButtonDown(0))
            {
                // 按下鼠标左键，进入拖拽状态，隐藏cursor
                activeSlider = slider;
                Cursor.visible = false;
            }
        }
        else
        {
            // 鼠标不悬停在滑块上
            slider.transform.localScale = slider == sliderA ? sliderAOriginalScale : sliderBOriginalScale;
        }
    }

    private void DragSlider(GameObject slider)
    {
        float mouseYDelta = Input.GetAxis("Mouse Y") * Time.deltaTime;
        slider.transform.Translate(0, mouseYDelta, 0);

        // 另一个滑块需要以相同速度反方向移动
        GameObject otherSlider = slider == sliderA ? sliderB : sliderA;
        otherSlider.transform.Translate(0, -mouseYDelta, 0);
    }
}
