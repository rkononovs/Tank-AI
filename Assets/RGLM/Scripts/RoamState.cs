
using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class RoamState : BaseState
    {
        private RGLMTank aiTank;

        public RoamState(RGLMTank aiTank)
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

