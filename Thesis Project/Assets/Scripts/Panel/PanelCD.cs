using System.Collections;
using System.Collections.Generic;
using Chronos;
using UnityEngine;

public class PanelCD : PanelUIElements
{
    private float lastAngle = 0f;
    private int consecutiveFrames = 0; // 连续帧计数器
    public int requiredConsecutiveFrames = 5; // 需要的连续帧数
    public float maxAnglePerFrame = 1f; // 每帧最大角度变化

    private bool isClockwise = true; // 当前旋转方向
    private float accumulatedRotation = 0f; // 累积的旋转角度

    public float startAngle = 0;
    public float minRotateAngle = -30;
    public float maxRotateAngle = 30;

    public float angularVelocity;
    public float idleAngularVelocity;
    private float currentIdleVelocity;

    private bool canSelfRotate = true;
    private AreaClock2D areaClock;

    protected override void Start()
    {
        base.Start();
        transform.rotation = Quaternion.Euler(0, 0, startAngle);
        currentIdleVelocity = idleAngularVelocity;

        areaClock = FindObjectOfType<AreaClock2D>();
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        currentIdleVelocity = idleAngularVelocity / 3;
    }

    protected override void Update()
    {
        base.Update();

        if (canSelfRotate)
        {
            transform.Rotate(0, 0, -currentIdleVelocity * Time.deltaTime);
            angularVelocity = -currentIdleVelocity;
            areaClock.localTimeScale = currentIdleVelocity / idleAngularVelocity;
            // print(angularVelocity);
        }
    }

    protected override void MouseDragAction()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (mouseDelta == Vector2.zero)
        {
            canSelfRotate = true;
            currentIdleVelocity = idleAngularVelocity / 3;
            print("angularVelocity " + currentIdleVelocity);
        }
        
        if (mouseDelta != Vector2.zero)
        {
            canSelfRotate = false;
            currentIdleVelocity = 0;

            areaClock.localTimeScale = isClockwise ? 3 : -3;
            
            float currentAngle = Mathf.Atan2(mouseDelta.y, mouseDelta.x) * Mathf.Rad2Deg;
            float angleDelta = Mathf.DeltaAngle(lastAngle, currentAngle);


            // angularVelocity = angleDelta / Time.deltaTime;
            // print("angularVelocity " + angularVelocity);

            // Quaternion lastRotation = transform.rotation;

            // 检查旋转方向是否一致
            bool isCurrentClockwise = angleDelta < 0;
            if (isCurrentClockwise == isClockwise || consecutiveFrames == 0)
            {
                consecutiveFrames++;
                accumulatedRotation += angleDelta;
            }
            else
            {
                // 重置计数器和累积角度
                consecutiveFrames = 1;
                accumulatedRotation = angleDelta;
            }

            // 更新旋转方向
            isClockwise = isCurrentClockwise;


            // 检查连续帧数是否达到阈值
            if (consecutiveFrames >= requiredConsecutiveFrames)
            {
                accumulatedRotation = Mathf.Clamp(accumulatedRotation, -maxAnglePerFrame, maxAnglePerFrame);

                // angularVelocity = accumulatedRotation / (Time.deltaTime * consecutiveFrames);
                // print("angularVelocity " + angularVelocity);
                // float currentRotationZ = transform.rotation.z + accumulatedRotation;

                // transform.rotation = Quaternion.Euler(0, 0,currentRotationZ);
                // transform.rotation = Quaternion.Euler(0, 0,Mathf.Clamp(currentRotationZ, minRotateAngle, maxRotateAngle));
                if (accumulatedRotation + transform.rotation.z > minRotateAngle &
                    accumulatedRotation + transform.rotation.z < maxRotateAngle)
                {
                    transform.Rotate(0, 0, accumulatedRotation);
                }
                Debug.Log(isClockwise ? "顺时针移动！" : "逆时针移动！");

                // 重置累积角度和计数器
                consecutiveFrames = 0;
                accumulatedRotation = 0f;
            }

            lastAngle = currentAngle;
        }
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        consecutiveFrames = 0;
        accumulatedRotation = 0f;
        currentIdleVelocity = idleAngularVelocity;
        canSelfRotate = true;
    }
}
