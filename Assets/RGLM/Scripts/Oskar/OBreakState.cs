using System;
using UnityEngine;

namespace RGLM
{
    public class OBreakState : BaseState
    {
        private OLazyTank aiTank;
        float t = 0;

        public OBreakState(OLazyTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            t = 0;
            aiTank.TankStopTank();
            aiTank.stats["breakState"] = true;
            return null;
        }

        public override Type StateExit()
        {
            aiTank.TankStartTank();
            aiTank.stats["breakState"] = false;
            return null;

        }

        public override Type StateUpdate()
        {
            t += Time.deltaTime;
            if (t > aiTank.breakTime)
                return typeof(OMoveState);

            aiTank.IsCollectableNearby();
            aiTank.IsEnemyNearby();

            foreach(Rule rule in aiTank.rules.GetRules)
            {
                if (rule.CheckRule(aiTank.stats) != null)
                    return rule.CheckRule(aiTank.stats);
            }

            return null;
        }
    }
}