using System;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class JSearchingState : BaseState
    {
        private float t = 0;

        private JTankV1 aiTank;

        public JSearchingState(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Searching state");
            return null;
        }

        public override Type StateExit()
        {
            return null;
        }

        public override Type StateUpdate()
        {
            //if low health or ammo, go searching
            if (aiTank.TankGetHealthLevel() < aiTank.LowHP || aiTank.TankGetAmmoLevel() < aiTank.LowAmmo || aiTank.TankGetFuelLevel() < aiTank.LowFuel || aiTank.needsResources)
            {
                if (aiTank.consumablesFound.Count > 0)
                {
                    aiTank.consumablePosition = aiTank.consumablesFound.First().Key;
                    aiTank.TankFollowPathToPoint(aiTank.consumablePosition, 1f);
                    t += Time.deltaTime;
                    if (t > 10)
                    {
                        aiTank.TankGenerateRandomPoint();
                        t = 0;
                    }
                    return typeof(JResourceGathering);
                }
                else
                {
                    aiTank.targetTankPosition = null;
                    aiTank.consumablePosition = null;
                    aiTank.basePosition = null;
                    aiTank.TankFollowPathToRandomPoint(1f);
                }
            }       

            //if there is a consumables
            else if (aiTank.consumablesFound.Count > 0)
            {
                return typeof(JResourceGathering);
            }
            
            //if there is a target found
            else if (aiTank.targetTanksFound.Count > 0 && aiTank.targetTanksFound.First().Key != null && !aiTank.needsResources)
            {
                return typeof(JGulagState);
            }

            else if (aiTank.basesFound.Count > 0)
            {
                aiTank.basePosition = aiTank.basesFound.First().Key;
                return typeof(JBaseWreakerState);
            }

            else
            {
                //searching
                aiTank.targetTankPosition = null;
                aiTank.consumablePosition = null;
                aiTank.basePosition = null;
                aiTank.TankFollowPathToRandomPoint(1f);
                t += Time.deltaTime;
                if (t > 10)
                {
                    aiTank.TankGenerateRandomPoint();
                    t = 0;
                }
            }
            return null;
        }
    }
}