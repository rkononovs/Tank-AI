using System;
using System.Linq;

namespace RGLM
{
    public class JResourceGathering : BaseState
    {
        private JTankV1 aiTank;

        public JResourceGathering(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Resource Gathering state");
            return null;
        }

        public override Type StateExit()
        {
            return null;
        }

        public override Type StateUpdate()
        {
            if (aiTank.consumablesFound.Count > 0) //Pick up items.
            {
                aiTank.consumablePosition = aiTank.consumablesFound.First().Key;
                aiTank.TankFollowPathToPoint(aiTank.consumablePosition, 1f);
            }
            else //No items.
            {
                if (aiTank.TankGetHealthLevel() >= aiTank.LowHP && aiTank.TankGetAmmoLevel() >= aiTank.LowAmmo && aiTank.TankGetFuelLevel() >= aiTank.LowFuel) //If there are resources.
                {
                    aiTank.needsResources = false;
                    if (aiTank.targetTanksFound.Count > 0 && !aiTank.needsResources) //If enemy is in range and tank have resources.
                    { 
                        return typeof(JGulagState); //State change - Gulag.
                    }
                }

                if (aiTank.TankGetFuelLevel() < aiTank.LowFuel)
                {
                    return typeof(JRotatingState); //State change - Rotate.
                }
                else
                {
                    if (aiTank.targetTanksFound.Count > 0 && !aiTank.needsResources) //If enemy is in range and tank have resources.
                    {
                        return typeof(JGulagState); //State change - Gulag.
                    }
                    else
                    {
                        return typeof(JFleeingState); //State change - Flee.
                    }
                }
            }
            return null;
        }
    }
}