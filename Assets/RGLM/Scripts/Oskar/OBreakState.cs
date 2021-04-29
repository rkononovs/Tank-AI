using System;
using UnityEngine;

//Breake state - tanks take a quick nap if there are no hazards

namespace RGLM
{
    public class OBreakState : BaseState
    {
        private OLazyTank aiTank; //refrence to tank
        float t = 0; //for counting tank

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
            if (t > aiTank.breakTime) // sleep for some time
                return typeof(OMoveState); // Then change state

            //In the meantime check surroundings
            aiTank.IsCollectableNearby();
            aiTank.IsEnemyNearby();

            //Look if any of the conditions for rules is met
            foreach(Rule rule in aiTank.rules.GetRules)
            {
                if (rule.CheckRule(aiTank.stats) != null)
                    return rule.CheckRule(aiTank.stats);
            }
            return null;
        }
    }
}