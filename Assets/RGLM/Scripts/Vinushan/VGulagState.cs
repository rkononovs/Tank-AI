
using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class VGulagState : BaseState
    {
        private VTankV1 aiTank;

        public VGulagState(VTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            Debug.Log("Gulag State");
            return null;
        }

        public override Type StateExit()
        {
            return null;

        }

        public override Type StateUpdate()
        {
            if (aiTank.targetTanksFound.Count > 0)
            {
                aiTank.targetTankPosition = aiTank.targetTanksFound.First().Key;

                if (aiTank.targetTankPosition == null)
                {
                    return typeof(VRotateState);
                }

                else
                {
                    aiTank.TankFollowPathToPoint(aiTank.targetTankPosition, 1);

                    aiTank.TankFireAtPoint(aiTank.targetTankPosition);

                    if (aiTank.TankGetHealthLevel() < 60 || aiTank.TankGetFuelLevel() < 60 || aiTank.TankGetAmmoLevel() < 60)
                    {
                        return typeof(VFleeingState);
                    }
                    else
                    {
                        return null;
                    }
                }
                
            }

            else
            {
                if (aiTank.TankGetHealthLevel() < 60 || aiTank.TankGetFuelLevel() < 60 || aiTank.TankGetAmmoLevel() < 60)
                {
                    return typeof(VGatheringState);
                }
                else
                {
                    aiTank.TankFollowPathToRandomPoint(1);
                    return typeof(VRotateState);
                }
            }

        }
    }
}
