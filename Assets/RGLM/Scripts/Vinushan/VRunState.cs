
using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class VRunState : BaseState
    {
        private VTankV1 aiTank;

        public VRunState(VTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            Debug.Log("Run State");
            aiTank.stats["RunState"] = true;
            return null;
        }

        public override Type StateExit()
        {
            aiTank.stats["RunState"] = false;
            return null;

        }

        public override Type StateUpdate()
        {
            aiTank.EnemyNear();

            if (aiTank.targetTanksFound.Count > 0)
            {
                return typeof(VFleeingState);
            }
            else
            {
                aiTank.TankFollowPathToRandomPoint(0.5f);
                return typeof(VSearchingState);
            }
        }
    }
}
