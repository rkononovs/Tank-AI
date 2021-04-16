using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class JBaseWreakerState : BaseState
    {
        private JTankV1 aiTank;

        public JBaseWreakerState(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Base Wreaker state");
            
            return null;
        }

        public override Type StateExit()
        {
            return null;

        }

        public override Type StateUpdate()
        {

            //if base if found
            if (aiTank.TankGetFuelLevel() <= aiTank.LowFuel || aiTank.TankGetHealthLevel() <= aiTank.LowHP || aiTank.TankGetFuelLevel() <= aiTank.LowAmmo)
            {
                aiTank.needsResources = true;
                return typeof(JFleeingState);
            }
            else
            {   
                if (aiTank.targetTanksFound.Count > 0 && !aiTank.needsResources)
                {
                    return typeof(JGulagState);
                }
                else
                {
                    aiTank.basePosition = aiTank.basesFound.First().Key;
                    if (aiTank.basePosition != null)
                    {
                        //go close to it and fire
                        if (Vector3.Distance(aiTank.transform.position, aiTank.basePosition.transform.position) < 25f)
                        {
                            aiTank.TankFireAtPoint(aiTank.basePosition);
                        }
                        else
                        {
                            aiTank.TankFollowPathToPoint(aiTank.basePosition, 1f);
                        }
                    }
                    else
                    {
                        return typeof(JFleeingState);
                    }
                }
            }
            return null;
        }
    }
}