using System;

//Gathering state - tanks goes to collectable at full speed

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

            if (aiTank.stats["collectableSpotted"]) // If sees something 
            {
                aiTank.TankFollowPathToPoint(aiTank.consumablePosition,1); //goes for it  at full speed
                return null;
            }
            else //Nothing in sight? take a break
                return typeof(OBreakState);
        }
    }
}