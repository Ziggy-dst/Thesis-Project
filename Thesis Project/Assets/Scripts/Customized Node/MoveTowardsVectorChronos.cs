using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Chronos;
using Unity.VisualScripting;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Movement/Direct")]
    [Description("Moves the agent towards to target Vector per frame without pathfinding in chronos timescale, return true if arrived")]
    public class MoveTowardsVectorChronos : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<Vector2> target;
        public BBParameter<float> speed = 2;
        public BBParameter<float> stopDistance = 0.1f;
        public bool waitActionFinish;
        private Timeline timeline;

        private Blackboard currentBlackboard;

        protected override string OnInit()
        {
            timeline = agent.GetComponent<Timeline>();
            currentBlackboard = agent.GetComponent<Blackboard>();
            return base.OnInit();
        }

        protected override void OnUpdate() {
            Debug.Log("------------------Move--------------------");
            Debug.Log("target " + target.value);
            Debug.Log("agent.position " + agent.position);
            // Debug.Log("distance " + ( agent.position - target.value ).magnitude);
            Debug.Log("vector2.distance " + Vector2.Distance( agent.position, target.value ));
            if ( Vector2.Distance( agent.position, target.value ) <= stopDistance.value )
            {
                currentBlackboard.SetVariableValue("isSliding", false);
                EndAction();
                return;
            }
            else
            {
                
            }

            agent.position = Vector3.MoveTowards(agent.position, target.value, speed.value * timeline.deltaTime);

            if ( !waitActionFinish ) {
                EndAction();
            }
        }
    }
}