using System;
using UnityEngine;

namespace RGLM
{
    public class JFleeingState : BaseState
    {
        private JTankV1 aiTank;

        private float t = 0;

        public JFleeingState(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Fleeing state");
            return null;
        }

        public override Type StateExit()
        {
            return null;

        }

        public override Type StateUpdate()
        {
            if (aiTank.targetTanksFound.Count > 0 && !aiTank.needsResources)
            {
                return typeof(JGulagState);
            }
            if (aiTank.consumablesFound.Count > 0)
            {
                return typeof(JResourceGathering);
            }
            else
            {
                aiTank.targetTankPosition = null;
                aiTank.consumablePosition = null;
                aiTank.basePosition = null;
                aiTank.TankFollowPathToRandomPoint(1f);
                t += Time.deltaTime;
                if (t > 10)
                {
                    aiTank.TankGenerateRandomPoint();
                    t = 0;
                    return typeof(JRotatingState);
                }
            }
            return null;
        }
    }
}