using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RGLM
{
    public class RKChaseState : BaseState
    {
        private BTTank aiTank;

        public RKChaseState(BTTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            throw new NotImplementedException();
        }

        public override Type StateExit()
        {
            throw new NotImplementedException();
        }

        public override Type StateUpdate()
        {
            if (Vector3.Distance(aiTank.transform.position, aiTank.targetTankPosition.transform.position) < 1f)
            {
                return typeof(RKAttackState);
            }
            else
            {
                aiTank.TankFollowPathToPoint(aiTank.targetTankPosition, 1f);
                return null;
            }
        }
    }
}

