﻿using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

    [Name("Set Layer Weight")]
    [Category("Animator")]
    public class MecanimSetLayerWeight : ActionTask<Animator>
    {

        public BBParameter<int> layerIndex;

        [SliderField(0, 1)]
        public BBParameter<float> layerWeight;

        [SliderField(0, 1)]
        public float transitTime;

        private float currentValue;

        protected override string info {
            get { return "Set Layer " + layerIndex + ", weight " + layerWeight; }
        }

        protected override void OnExecute() {

            currentValue = agent.GetLayerWeight(layerIndex.value);
        }

        protected override void OnUpdate() {
            var norm = transitTime > 0f ? Mathf.Lerp(currentValue, layerWeight.value, elapsedTime / transitTime) : layerWeight.value;
            agent.SetLayerWeight(layerIndex.value, norm);

            if ( elapsedTime >= transitTime ) {
                EndAction(true);
            }
        }
    }
}