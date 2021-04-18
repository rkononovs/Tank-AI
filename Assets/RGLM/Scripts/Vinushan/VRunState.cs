
using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class VRunState : BaseState
    {
        private RGLMTank aiTank;

        public VRunState(RGLMTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            Debug.Log("Run State");
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
