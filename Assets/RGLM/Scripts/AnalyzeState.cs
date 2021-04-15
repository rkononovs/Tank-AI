
using System;

namespace RGLM
{
    public class AnalyzeState : BaseState
    {
        private AITank aiTank;

        public AnalyzeState(AITank aiTank)
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

