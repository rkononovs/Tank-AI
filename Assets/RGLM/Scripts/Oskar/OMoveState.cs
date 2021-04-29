using System;
using UnityEngine;

//Moving state - tank slowly moves around the map

namespace RGLM
{
    public class OMoveState : BaseState
    {
        private OLazyTank aiTank;
        float t = 0;

        public OMoveState(OLazyTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            aiTank.stats["movingState"] = true;
            t = 0;
            return null;
        }

        public override Type StateExit()
        {
            aiTank.stats["movingState"] = false;
            return null;

        }

        public override Type StateUpdate()
        {
            aiTank.IsCollectableNearby();

            t += Time.deltaTime;
            if (t > aiTank.breakTime*1.2f) // After some time
                return typeof(OBreakState); // have a break

            aiTank.TankFollowPathToRandomPoint(0.3f); // move around a little 

            //Look if any of the conditions for rules is met
            foreach (Rule rule in aiTank.rules.GetRules)
            {
                if (rule.CheckRule(aiTank.stats) != null)
                    return rule.CheckRule(aiTank.stats);
            }
            return null;
        }
    }
}