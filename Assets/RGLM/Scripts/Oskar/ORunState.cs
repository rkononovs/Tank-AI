using System;
using UnityEngine;

namespace RGLM
{
    public class ORunState : BaseState
    {
        private OLazyTank aiTank;
        float t = 0;
        public ORunState(OLazyTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            t = 0;
            aiTank.stats["runState"] = true;
            return null;
        }

        public override Type StateExit()
        {
            aiTank.stats["runState"] = false;
            return null;

        }

        public override Type StateUpdate()
        {
            t += Time.deltaTime;
            if (t > aiTank.runTime)
            {
                aiTank.TankFollowPathToRandomPoint(1f);
            }
            else
                return typeof(OBreakState);

            foreach(Rule rule in aiTank.rules.GetRules)
            {
                if (rule.CheckRule(aiTank.stats) != null)
                    return rule.CheckRule(aiTank.stats);
            }
            return null;
        }
    }
}