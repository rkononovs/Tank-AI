using System;
using System.Linq;

namespace RGLM
{
    public class JRotatingState : BaseState
    {
        private JTankV1 aiTank;

        public JRotatingState(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Rotating state");
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
            if (aiTank.consumablesFound.Count > 0)
            {
                return typeof(JResourceGathering);
            }
            if (aiTank.targetTanksFound.Count > 0)
            {
                if (aiTank.needsResources)
                {
                    return typeof(JFleeingState);
                }
                else
                {
                    return typeof(JGulagState);
                }
            }
            if (!aiTank.isRotating)
            {
                return typeof(JSearchingState);
            }
            return null;
        }
    }
}