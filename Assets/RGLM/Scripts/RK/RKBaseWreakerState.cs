using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class RKBaseWreakerState : BaseState
    {
        private BTTank aiTank;

        public RKBaseWreakerState(BTTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Romans Base Wreaker state");
            return null;
        }

        public override Type StateExit()
        {
            return null;
        }

        public override Type StateUpdate()
        {
            
            if (aiTank.TankGetFuelLevel() <= aiTank.LowFuel || aiTank.TankGetHealthLevel() <= aiTank.LowHP || aiTank.TankGetFuelLevel() <= aiTank.LowAmmo) //If resources are low.
            {
                aiTank.needsResources = true; //Need resources.
                return typeof(RKFleeingState); //State change - Fleeing.
            }
            else
            {   
                if (aiTank.targetTanksFound.Count > 0 && !aiTank.needsResources) //If enemy tank in sight and have enough resources.
                {
                    return typeof(RKGulagState); //State change - Gulag.
                }
                else //Enemy tank too far.
                {
                    aiTank.basePosition = aiTank.basesFound.First().Key; //Set enemy base position as target.
                    if (aiTank.basePosition != null)
                    {
                        
                        if (Vector3.Distance(aiTank.transform.position, aiTank.basePosition.transform.position) < 25f) //If enemy base in range.
                        {
                            aiTank.TankFireAtPoint(aiTank.basePosition); //Shoot at enemy base.
                        }
                        else
                        {
                            aiTank.TankFollowPathToPoint(aiTank.basePosition, 1f); //Get closer to enemy base.
                        }
                    }
                    else
                    {
                        return typeof(RKFleeingState); //State change - Fleeing.
                    }
                }
            }
            return null;
        }
    }
}