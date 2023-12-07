using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FixNavMeshAgentRotation : MonoBehaviour
{
	void Start()	{
		var agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;
	}

	private void Update()
	{
		Vector3 pos3D = transform.position;
		transform.position = new Vector3(pos3D.x, pos3D.y, 0);
	}
}
