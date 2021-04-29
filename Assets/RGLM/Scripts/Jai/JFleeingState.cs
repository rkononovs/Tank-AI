using System;
using UnityEngine;

namespace RGLM
{
    public class JFleeingState : BaseState
    {
        private JTankV1 aiTank;

        private float t = 0; //Time passed counter.

        public JFleeingState(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            //UnityEngine.Debug.LogError("Entered State - Jai Fleeing state"); Used for debugging
            return null;
        }

        public override Type StateExit()
        {
            return null;

        }

        public override Type StateUpdate()
        {
            if (aiTank.targetTanksFound.Count > 0 && !aiTank.needsResources) //If enemy in sight and have enough resources.
            {
                return typeof(JGulagState); //State change - Gulag.
            }
            if (aiTank.consumablesFound.Count > 0) //If consumables in sight.
            {
                return typeof(JResourceGathering); //State change - Resource gathering.
            }
            else
            {
                aiTank.targetTankPosition = null;
                aiTank.consumablePosition = null;
                aiTank.basePosition = null;
                aiTank.TankFollowPathToRandomPoint(1f);
                t += Time.deltaTime;
                if (t > 10) //Wait 10s.
                {
                    aiTank.TankGenerateRandomPoint();
                    t = 0;
                    return typeof(JRotatingState); //State change - Rotating.
                }
            }
            return null;
        }
    }
}