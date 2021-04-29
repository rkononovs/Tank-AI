using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace RGLM
{
    public class RKAttack : BaseState
    {
        private BTTank aiTank;

        private float t = 0; //Time passed counter.

        public RKAttack(BTTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            // Debug.Log("Entered State - Romans Attack"); // For DEBUG PURPOSES
            return null;
        }

        public override Type StateExit()
        {
            return null;
        }

        public override Type StateUpdate()
        {
            if (aiTank.TankGetFuelLevel() <= aiTank.LowFuel || aiTank.TankGetHealthLevel() <= aiTank.LowHP || aiTank.TankGetAmmoLevel() <= aiTank.LowAmmo) //If resources are low.
            {
                aiTank.needsResources = true; //Tank is low on resources.
                return typeof(RKFleeingState); //State change - Fleeing.
            }
            else
            {
                if (aiTank.targetTanksFound.Count > 0 && aiTank.targetTanksFound.First().Key != null) //If enemy tank is in sight.
                {
                    aiTank.targetTankPosition = aiTank.targetTanksFound.First().Key; //Set enemy position as target destination.
                    if (aiTank.targetTankPosition != null)
                    {
                        if (Vector3.Distance(aiTank.transform.position, aiTank.targetTankPosition.transform.position) < 25f) //If enemy is in range
                        {
                            aiTank.TankFireAtPoint(aiTank.targetTankPosition); //Shoot at enemy.
                        }

                        else
                        {
                            aiTank.TankFollowPathToPoint(aiTank.targetTankPosition, 1f); //Get closer to enemy.
                            t += Time.deltaTime;
                            if (t > 10) //Wait 10s.
                            {
                                t = 0;
                                return typeof(RKRotatingState); //State change - Rotating.
                            }
                        }
                    }
                }
                else
                {
                    aiTank.TankFollowPathToPoint(aiTank.targetTankPosition, 1f); //Get closer to enemy.
                    if (Vector3.Distance(aiTank.transform.position, aiTank.targetTankPosition.transform.position) < 2f)
                    {
                        return typeof(RKFleeingState); //State change - Fleeing.
                    }
                }
            }
            return null;
        }
    }
}