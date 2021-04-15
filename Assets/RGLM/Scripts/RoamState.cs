
using System;
using System.Diagnostics;
using UnityEngine;

namespace RGLM
{
    public class RoamState : BaseState
    {
        private AITank aiTank;

        public RoamState(AITank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            return null;
        }

        public override Type StateExit()
        {
            return null;

        }

        public override Type StateUpdate()
        {
            return null;
        }
    }
}

