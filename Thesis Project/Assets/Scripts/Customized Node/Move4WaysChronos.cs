using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using Chronos;
using Unity.VisualScripting;

namespace NodeCanvas.Tasks.Actions
{

    [Category("Movement/Direct")]
    [Description("Moves the agent towards to target per frame without pathfinding in chronos timescale")]
    public class Move4WaysChronos : ActionTask<Transform>
    {
        public enum Direction
        {
            up,
            down,
            left,
            right
        }
        
        [RequiredField]
        public BBParameter<Direction> direction;
        public BBParameter<float> distance;
        public BBParameter<float> speed = 2;
        public BBParameter<float> stopDistance = 0.1f;
        public bool waitActionFinish;
        private Timeline timeline;
        private Vector3 target;

        protected override string OnInit()
        {
            timeline = agent.GetComponent<Timeline>();
            return base.OnInit();
        }

        protected override void OnExecute()
        {
            switch (direction.value)
            {
                case Direction.up:
                    target = new Vector2(agent.position.x, distance.value);
                    break;
                case Direction.down:
                    target = new Vector2(agent.position.x, -distance.value);
                    break;
                case Direction.left:
                    target = new Vector2(-distance.value, agent.position.y);
                    break;
                case Direction.right:
                    target = new Vector2(distance.value, agent.position.y);
                    break;
            }
        }

        protected override void OnUpdate()
        {
            if ((agent.position - target).magnitude <= stopDistance.value) 
            {
                EndAction();
                return;
            }

            agent.position = Vector3.MoveTowards(agent.position, target, speed.value * timeline.deltaTime);
            
            if (!waitActionFinish)
            {
                EndAction();
            }
        }
    }
}