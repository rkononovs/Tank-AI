using System;
using System.Collections.Generic;
using UnityEngine;


namespace RGLM
{
    public class VTankV1 : RGLMTank
    {
        //State Machine Section
        void InitializeStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states.Add(typeof(VRunState), new VRunState(this));
            states.Add(typeof(VRotateState), new VRotateState(this));
            states.Add(typeof(VSearchingState), new VSearchingState(this));
            states.Add(typeof(VGulagState), new VGulagState(this));
            states.Add(typeof(VGatheringState), new VGatheringState(this));
            states.Add(typeof(VFleeingState), new VFleeingState(this));
            GetComponent<StateMachine>().SetStates(states);
        }


        public override void AITankStart()
        {

        }

        public override void AITankUpdate()
        {
            targetTanksFound = GetAllTargetTanksFound;
            consumablesFound = GetAllConsumablesFound;
            basesFound = GetAllBasesFound;
        }

        public override void AIOnCollisionEnter(Collision collision)
        {

        }

    }

}