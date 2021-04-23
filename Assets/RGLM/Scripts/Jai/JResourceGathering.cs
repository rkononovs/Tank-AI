using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class JResourceGathering : BaseState
    {
        private JTankV1 aiTank;

        private float t = 0; //Time passed counter.

        public JResourceGathering(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Jai Resource Gathering state");
            return null;
        }

        public override Type StateExit()
        {
            return null;
        }

        public override Type StateUpdate()
        {
            if (aiTank.consumablesFound.Count > 0) //If resource found.
            {
                //Pick up items.
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

                if (aiTank.TankGetFuelLevel() < aiTank.LowFuel) //If fuel is low
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
                        t += Time.deltaTime;//Wait for 1s to change state(give enough time to notice the resource).
                        if (t > 1)
                        {
                            t = 0;
                            return typeof(JFleeingState); //State change - Flee.
                        }
                    }
                }
            }
            return null;
        }
    }
}