using System;
using UnityEngine;

namespace RGLM
{
    public class OGatherState : BaseState
    {
        private OLazyTank aiTank;
        float t = 0;
        public OGatherState(OLazyTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            t = 0;
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
            if (!aiTank.CollectableNearby())
                return typeof(OBreakState);
            return null;
        }
    }
}