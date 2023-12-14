using System;
using System.Collections;
using System.Collections.Generic;
using NodeCanvas.BehaviourTrees;
using NodeCanvas.Framework;
using UnityEngine;
 
//该物体需要 LineRenderer组件，也可以手动添加LineRenderer组件，把这个注释掉
[RequireComponent(typeof(LineRenderer))]
public class RadiusDisplay : MonoBehaviour
{
    public int segments;//所用的线条（线条越多，画出来的圆越圆）
    // X轴与Y轴半径相等时绘制的是正圆（正多边形），否则是椭圆；同时也会影响圆的大小
    public float xradius;//X轴 半径
    public float yradius;//Y轴 半径
    public float zradius;
    LineRenderer line;
    
    private BehaviourTreeOwner behaviourTree;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = (segments + 1);//设置 LineRenderer 组件的画圆线条的数量
        line.useWorldSpace = false;//不使用世界坐标
        CreatePoints();
        
        behaviourTree = GetComponentInParent<BehaviourTreeOwner>();
    }

    private void Update()
    {
        transform.localScale = Vector3.one * behaviourTree.blackboard.GetVariable<float>("Attack Range").value;
    }

    void CreatePoints()//创建圆
    {
        float x;
        float y = 0;
        float z;
        float angle = 0;
        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * zradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            line.SetPosition(i, new Vector3(x, y, z));//设置每个点的坐标
 
            angle += (360f / segments);
        }
    }
}