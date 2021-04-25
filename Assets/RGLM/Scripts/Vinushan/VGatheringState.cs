
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
            aiTank.stats["GatheringState"] = true;
            return null;
        }

        public override Type StateExit()
        {
            aiTank.stats["GatheringState"] = false;
            return null;

        }

        public override Type StateUpdate()
        {
            aiTank.ResourceNear();

            if (aiTank.stats["ResourceNear"])
            {
                aiTank.consumablePosition = aiTank.consumablesFound.First().Key;

                aiTank.TankFollowPathToPoint(aiTank.consumablePosition, 0.5f);
            }

            else
            {
                return typeof(VSearchingState);
            }

            return null;

        }
    }
}
