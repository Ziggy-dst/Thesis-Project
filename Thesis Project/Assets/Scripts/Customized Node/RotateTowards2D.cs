using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    [Category("Movement/Direct")]
    [Description("Rotate the agent towards the target per frame")]
    public class RotateTowards2D : ActionTask<Transform>
    {

        [RequiredField]
        public BBParameter<GameObject> target;
        public BBParameter<float> speed = 20;
        [SliderField(1, 180)]
        public BBParameter<float> angleDifference = 5;
        // public BBParameter<Vector3> upVector = Vector3.forward;
        public bool waitActionFinish;

        protected override void OnUpdate() {
            if (Vector2.Angle(target.value.transform.position - agent.position, agent.up) <= angleDifference.value) {
                EndAction();
                return;
            }

            var dir = target.value.transform.position - agent.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90; // 计算目标方向的角度
            agent.rotation = Quaternion.Euler(0, 0, Mathf.MoveTowardsAngle(agent.rotation.eulerAngles.z, angle, speed.value * Time.deltaTime));

            if (!waitActionFinish) {
                EndAction();
            }
        }
    }
}