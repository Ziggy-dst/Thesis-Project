using System.Collections.Generic;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using DG.Tweening;

using NavMeshAgent = UnityEngine.AI.NavMeshAgent;

namespace NodeCanvas.Tasks.Actions
{

    [Category("Movement/Pathfinding")]
    [Description("Move Randomly or Progressively between various game object positions taken from the list provided")]
    public class PatrolAndRotateTowards2D : ActionTask<NavMeshAgent>
    {

        public enum PatrolMode
        {
            Progressive,
            Random
        }

        [RequiredField, Tooltip("A list of gameobjects patrol points.")]
        public BBParameter<List<GameObject>> targetList;
        [Tooltip("The mode to use for patrol (progressive or random)")]
        public BBParameter<PatrolMode> patrolMode = PatrolMode.Random;
        public BBParameter<float> speed = 4;
        public BBParameter<float> keepDistance = 0.1f;

        private int index = -1;
        private Vector3? lastRequest;

        // add rotate towards function
        public bool repeat = false;

        // public BBParameter<float> rotateSpeed = 2;
        // [SliderField(1, 180)]
        // public BBParameter<float> angleDifference = 5;
        // public bool waitActionFinish;

        protected override string info {
            get { return string.Format("{0} Patrol {1}", patrolMode, targetList); }
        }

        protected override void OnExecute() {

            if ( targetList.value.Count == 0 ) {
                EndAction(false);
                return;
            }

            if ( targetList.value.Count == 1 ) {

                index = 0;

                DoLook();

            } else {

                if ( patrolMode.value == PatrolMode.Random ) {
                    var newIndex = index;
                    while ( newIndex == index ) {
                        newIndex = Random.Range(0, targetList.value.Count);
                    }
                    index = newIndex;

                    DoLook();
                }

                if ( patrolMode.value == PatrolMode.Progressive ) {
                    index = (int)Mathf.Repeat(index + 1, targetList.value.Count);

                    DoLook();
                }
            }

            var targetGo = targetList.value[index];
            if ( targetGo == null ) {
                ParadoxNotion.Services.Logger.LogWarning("List game object is null on Patrol Action Task.", LogTag.EXECUTION, this);
                EndAction(false);
                return;
            }

            var targetPos = targetGo.transform.position;

            agent.speed = speed.value;
            if ( ( agent.transform.position - targetPos ).magnitude < agent.stoppingDistance + keepDistance.value ) {
                EndAction(true);
                return;
            }
        }

        void DoLook()
        {
            Debug.Log(targetList.value[index].transform.position);
            Vector2 lookDirection = targetList.value[index].transform.position - agent.transform.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90; // 计算角度

            Vector3 rotation = new Vector3(0f, 0f, angle);
            agent.transform.DORotate(rotation, 0.2f);
            // Vector2 lookDirection = targetList.value[index].transform.position - agent.transform.position;
            // float angle = Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg;
            // agent.transform.LookAt(targetList.value[index].transform.position);
            // agent.transform.rotation = Quaternion.Euler(0, 0, angle);

            if (!repeat)
                EndAction(true);
        }


        protected override void OnUpdate() {
            var targetPos = targetList.value[index].transform.position;
            if ( lastRequest != targetPos ) {
                if ( !agent.SetDestination(targetPos) ) {
                    EndAction(false);
                    return;
                }
            }

            lastRequest = targetPos;

            if ( !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + keepDistance.value ) {
                EndAction(true);
            }

            // if (Vector2.Angle(targetList.value[0].transform.position - agent.transform.position, agent.transform.up) <= angleDifference.value) {
            //     EndAction();
            //     return;
            // }
            //
            // var dir = targetList.value[0].transform.position - agent.transform.position;
            // var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90; // 计算目标方向的角度
            // agent.transform.rotation = Quaternion.Euler(0, 0, Mathf.MoveTowardsAngle(agent.transform.rotation.eulerAngles.z, angle, rotateSpeed.value * Time.deltaTime));
            //
            // if (!waitActionFinish) {
            //     EndAction();
            // }
        }

        protected override void OnPause() { OnStop(); }
        protected override void OnStop() {
            if ( lastRequest != null && agent.gameObject.activeSelf ) {
                agent.ResetPath();
            }
            lastRequest = null;
        }

        public override void OnDrawGizmosSelected() {
            if ( agent && targetList.value != null ) {
                foreach ( var go in targetList.value ) {
                    if ( go != null ) {
                        Gizmos.DrawSphere(go.transform.position, 0.1f);
                    }
                }
            }
        }
    }
}