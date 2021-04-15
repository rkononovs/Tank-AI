
using System;

namespace RGLM
{
    public class RunState : BaseState
    {
        private RGLMTank aiTank;

        public RunState(RGLMTank aiTank)
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

