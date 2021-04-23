using System;
using UnityEngine;

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
            t += Time.deltaTime;
            if (t > aiTank.breakTime*2)
                return typeof(OBreakState);

            aiTank.TankFollowPathToRandomPoint(0.3f);
            foreach(Rule rule in aiTank.rules.GetRules)
            {
                if (rule.CheckRule(aiTank.stats) != null)
                    return rule.CheckRule(aiTank.stats);
            }
            return null;
        }
    }
}