
using System;
using System.Diagnostics;
using UnityEngine;

namespace RGLM
{
    public class DefaultState : BaseState
    {
        private RGLMTank aiTank;

        public DefaultState(RGLMTank aiTank)
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
            return typeof(AnalyzeState);
        }
    }
}

