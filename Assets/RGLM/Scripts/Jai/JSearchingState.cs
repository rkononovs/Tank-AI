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

            if (aiTank.TankGetHealthLevel() < aiTank.LowHP || aiTank.TankGetAmmoLevel() < aiTank.LowAmmo || aiTank.TankGetFuelLevel() < aiTank.LowFuel || aiTank.needsResources) //If resources are low.
            {
                if (aiTank.consumablesFound.Count > 0) //If counsumable is in sight.
                {
                    //Move to consumable.
                    aiTank.consumablePosition = aiTank.consumablesFound.First().Key;
                    aiTank.TankFollowPathToPoint(aiTank.consumablePosition, 1f);
                    t += Time.deltaTime;
                    if (t > 10) //Wait for 10s.
                    {
                        aiTank.TankGenerateRandomPoint();
                        t = 0;
                    }
                    return typeof(JResourceGathering); //State change - Resource gathering.
                }
                else
                {
                    // I think this was causing the problem with fleeing we found with Jai ~Artur
                    //aiTank.targetTankPosition = null;
                    //aiTank.consumablePosition = null;
                    //aiTank.basePosition = null;
                    aiTank.TankFollowPathToRandomPoint(1f);
                }
            }       

            else if (aiTank.consumablesFound.Count > 0) //If consumable is in sight.
            {
                return typeof(JResourceGathering); //State change - Resource gathering.
            }
            
            else if (aiTank.targetTanksFound.Count > 0 && aiTank.targetTanksFound.First().Key != null && !aiTank.needsResources) //If enemy in isght and have enough resources.
            {
                return typeof(JGulagState); //State change - Gulag.
            }

            else if (aiTank.basesFound.Count > 0) //If enemy base in sight.
            {
                aiTank.basePosition = aiTank.basesFound.First().Key; //Set enemy base as target.
                return typeof(JBaseWreakerState); //State change - Base wrecker.
            }

            else //Keep moving.
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
                }
            }
            return null;
        }
    }
}