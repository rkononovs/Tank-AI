
using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class VSearchingState : BaseState
    {
        private VTankV1 aiTank;

        public VSearchingState(VTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            Debug.Log("Searching State");
            aiTank.stats["SearchingState"] = true;
            return null;
        }

        public override Type StateExit()
        {
            aiTank.stats["SearchingState"] = false;
            return null;

        }

        public override Type StateUpdate()
        {
            aiTank.ResourceNear();
            aiTank.EnemyNear();

            if (aiTank.stats["EnemyNear"])
            {
                return typeof(VFleeingState);
            }
            else
            {
                if (aiTank.stats["ResourceNear"])
                {
                    return typeof(VGatheringState);
                }
                else
                {
                    return typeof(VRunState);
                }
            }


        }
    }
}
