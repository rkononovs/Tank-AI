
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
            aiTank.stats["FleeingState"] = true;
            return null;
        }

        public override Type StateExit()
        {
            aiTank.stats["FleeingState"] = false;
            return null;

        }

        public override Type StateUpdate()
        {
            aiTank.EnemyNear();

            if (aiTank.stats["EnemyNear"])
            {
                aiTank.TankFollowPathToRandomPoint(1f);
            }
            else
            {
                return typeof(VGatheringState);
            }

            return null;

        }
    }
}
