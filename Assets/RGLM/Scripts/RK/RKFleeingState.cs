using System;
using UnityEngine;

namespace RGLM
{
    public class RKFleeingState : BaseState
    {
        private BTTank aiTank;
        private float t = 0; //Time passed counter.

        public RKFleeingState(BTTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            // UnityEngine.Debug.LogError("Entered State - Romans Fleeing state"); // For DEBUG PURPOSES
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
                return typeof(RKGulagState); //State change - Gulag.
            }
            if (aiTank.consumablesFound.Count > 0) //If consumables in sight.
            {
                return typeof(RKResourceGathering); //State change - Resource gathering.
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
                    aiTank.TankGenerateRandomPoint(); // ? Not sure if this is necessary since you are changing the state anyway ?
                    t = 0;
                    return typeof(RKRotatingState); //State change - Rotating.
                }
            }
            return null;
        }
    }
}