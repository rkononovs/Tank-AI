using System;
using UnityEngine;

namespace RGLM
{
    public class OGatherState : BaseState
    {
        private OLazyTank aiTank;
        public OGatherState(OLazyTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            aiTank.stats["gatherState"] = true;
            return null;
        }

        public override Type StateExit()
        {
            aiTank.stats["gatherState"] = false;
            return null;

        }

        public override Type StateUpdate()
        {
            aiTank.IsCollectableNearby();

            if (aiTank.stats["collectableSpotted"])
            {
                aiTank.TankFollowPathToPoint(aiTank.consumablePosition,1);
                return null;
            }
            else
                return typeof(OBreakState);
        }
    }
}