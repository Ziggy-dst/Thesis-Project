using NodeCanvas.Framework;
using ParadoxNotion;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions
{

    [Name("Player on Left or Right 2D")]
    [Category("GameObject")]
    public class CheckPlayerLeftorRight2D : ConditionTask<Transform>
    {

        [RequiredField]
        public BBParameter<GameObject> checkTarget;

        protected override bool OnCheck()
        {
            bool isOnRight = checkTarget.value.transform.position.x > agent.transform.position.x;
            return isOnRight;
        }
        
    }
}