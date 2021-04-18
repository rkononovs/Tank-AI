
using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class VGatheringState : BaseState
    {
        private VTankV1 aiTank;

        public VGatheringState(VTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            Debug.Log("Gathering State");
            return null;
        }

        public override Type StateExit()
        {
            return null;

        }

        public override Type StateUpdate()
        {

            if (aiTank.TankGetHealthLevel() < 60 || aiTank.TankGetFuelLevel() < 60 || aiTank.TankGetAmmoLevel() < 60)
            {
                if (aiTank.consumablesFound.Count > 0)
                {
                    aiTank.consumablePosition = aiTank.consumablesFound.First().Key;

                    aiTank.TankFollowPathToPoint(aiTank.consumablePosition, 1);
                }
                else
                {
                    aiTank.TankFollowPathToRandomPoint(1);
                }
            }
            else
            {
                return typeof(VRotateState);
            }

            return null;
        }
    }
}
