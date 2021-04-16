using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class RKRoamState : BaseState
    {
        float t = 0;
        private BTTank aiTank;

        public RKRoamState(BTTank aiTank)
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
            if (aiTank.targetTanksFound.Count > 0 && aiTank.targetTanksFound.First().Key != null)
            {
                aiTank.targetTankPosition = aiTank.targetTanksFound.First().Key;
                if (aiTank.targetTankPosition != null)
                {
                    if (Vector3.Distance(aiTank.transform.position, aiTank.targetTankPosition.transform.position) < 1.0f)
                    {
                        Debug.Log("Enter Chase state");
                        return typeof(RKChaseState);
                    }
                }
            }
            else
            {
                aiTank.TankFollowPathToRandomPoint(1f);
                t += Time.deltaTime;
                if (t > 10)
                {
                    aiTank.TankGenerateRandomPoint();
                    t = 0;
                }
            }
            return null;
        }
    }
}
