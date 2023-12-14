using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Chronos;

namespace NodeCanvas.Tasks.Actions
{

    [Category("Movement/Direct")]
    [Description("Moves the agent away from target per frame without pathfinding in Chronos timescale")]
    public class MoveAwayChronos : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<GameObject> target;
        public BBParameter<float> speed = 2;
        public BBParameter<float> stopDistance = 3;
        public bool waitActionFinish;
        private Timeline timeline;

        protected override string OnInit()
        {
            timeline = agent.GetComponent<Timeline>();
            return base.OnInit();
        }

        protected override void OnUpdate() {
            if ( ( agent.position - target.value.transform.position ).magnitude >= stopDistance.value ) {
                EndAction();
                return;
            }

            agent.position = Vector3.MoveTowards(agent.position, target.value.transform.position, -speed.value * timeline.deltaTime);
            if ( !waitActionFinish ) {
                EndAction();
            }
        }
    }
}