using System.Collections;
using Chronos;
using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using ParadoxNotion.Services;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    [Category("✫ Utility")]
    [Description(
        "Send a graph event. If global is true, all graph owners in scene will receive this event. Use along with the 'Check Event' Condition")]
    public class SendEventCoroutine : ActionTask<GraphOwner>
    {

        [RequiredField] public BBParameter<string> eventName;
        public BBParameter<float> delay;
        public bool sendGlobal;
        private Timeline timeline;

        protected override string OnInit()
        {
            timeline = agent.GetComponent<Timeline>();
            return base.OnInit();
        }

        protected override string info
        {
            get
            {
                return (sendGlobal ? "Global " : "") + "Send Event [" + eventName + "]" +
                       (delay.value > 0 ? " after " + delay + " sec." : "");
            }
        }

        protected override void OnExecute()
        {
            base.OnExecute();
            StartCoroutine(SendEvent());
            EndAction();
        }

        IEnumerator SendEvent()
        {
            yield return timeline.WaitForSeconds(delay.value);
            if (sendGlobal)
            {
                Graph.SendGlobalEvent(eventName.value, null, this);
            }
            else
            {
                agent.SendEvent(eventName.value, null, this);
            }
        }
    }

    ///----------------------------------------------------------------------------------------------

    [Category("✫ Utility")]
    [Description(
        "Send a graph event with T value. If global is true, all graph owners in scene will receive this event. Use along with the 'Check Event' Condition")]
    public class SendEventCoroutine<T> : ActionTask<GraphOwner>
    {

        [RequiredField] public BBParameter<string> eventName;
        public BBParameter<T> eventValue;
        public BBParameter<float> delay;
        public bool sendGlobal;
        private Timeline timeline;

        protected override string OnInit()
        {
            timeline = agent.GetComponent<Timeline>();
            return base.OnInit();
        }

        protected override string info
        {
            get
            {
                return string.Format("{0} Event [{1}] ({2}){3}", (sendGlobal ? "Global " : ""), eventName, eventValue,
                    (delay.value > 0 ? " after " + delay + " sec." : ""));
            }
        }

        protected override void OnExecute()
        {
            base.OnExecute();
            StartCoroutine(SendEvent<T>());
            EndAction();
        }

        IEnumerator SendEvent<T>()
        {
            yield return timeline.WaitForSeconds(delay.value);
            if (sendGlobal)
            {
                Graph.SendGlobalEvent(eventName.value, eventValue.value, this);
            }
            else
            {
                agent.SendEvent(eventName.value, eventValue.value, this);
            }
        }
    }
}