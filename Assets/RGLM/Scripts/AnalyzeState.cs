
using System;


namespace RGLM
{
    public class AnalyzeState : BaseState
    {
        private RGLMTank aiTank;

        public AnalyzeState(RGLMTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            aiTank.Rotate360();
            return null;
        }

        public override Type StateExit()
        {
            return null;

        }

        public override Type StateUpdate()
        {
            aiTank.TankFaceTurretToPoint(aiTank.lookAtPosition.transform.position);
            return null;
        }
    }
}

