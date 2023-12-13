using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using Chronos;


namespace NodeCanvas.Tasks.Actions
{

    [Category("✫ Utility")]
    public class WaitChronos : ActionTask
    {

        public BBParameter<float> waitTime = 1f;
        public CompactStatus finishStatus = CompactStatus.Success;
        private float timePassed;
        private Timeline timeline;

        protected override string OnInit()
        {
            timeline = agent.GetComponent<Timeline>();
            return base.OnInit();
        }

        protected override string info {
            get { return string.Format("Wait {0} timeline sec.", waitTime); }
        }

        protected override void OnExecute()
        {
            timePassed = 0;
            base.OnExecute();
        }

        protected override void OnUpdate()
        {
            timePassed += timeline.deltaTime;
            if ( timePassed >= waitTime.value ) {
                EndAction(finishStatus == CompactStatus.Success ? true : false);
            }
        }
    }
}