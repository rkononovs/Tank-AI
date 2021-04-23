
using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class VSearchingState : BaseState
    {
        private VTankV1 aiTank;

        public VSearchingState(VTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            Debug.Log("Searching State");
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
                return typeof(VGulagState);
            }
            else
            {
                if (aiTank.TankGetFuelLevel() < 60 || aiTank.TankGetHealthLevel() < 60 || aiTank.TankGetAmmoLevel() < 60)
                {
                    return typeof(VGatheringState);
                }
                else
                {
                    aiTank.TankFollowPathToRandomPoint(1);
                    return typeof(VRotateState);
                }
            }


            //return null;
        }
    }
}
