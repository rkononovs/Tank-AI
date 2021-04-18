
using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class VFleeingState : BaseState
    {
        private VTankV1 aiTank;

        public VFleeingState(VTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            Debug.Log("Fleeing State");
            return null;
        }

        public override Type StateExit()
        {
            return null;

        }

        public override Type StateUpdate()
        {
            aiTank.TankFollowPathToRandomPoint(1);

            if (aiTank.TankGetHealthLevel() < 60 || aiTank.TankGetFuelLevel() < 60 || aiTank.TankGetAmmoLevel() < 60)
            {
                return typeof(VGatheringState);
            }

            return typeof(VRotateState);

        }
    }
}
