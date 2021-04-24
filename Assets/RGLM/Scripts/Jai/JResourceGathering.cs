using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class JResourceGathering : BaseState
    {
        private JTankV1 aiTank;

        private float t = 0; //Time passed counter.

        public JResourceGathering(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Jai Resource Gathering state");
            return null;
        }

        public override Type StateExit()
        {
            return null;
        }

        public override Type StateUpdate()
        {
            if (aiTank.consumablesFound.Count > 0) //If resource found.
            {
                //Pick up items.
                aiTank.consumablePosition = aiTank.consumablesFound.First().Key;
                aiTank.TankFollowPathToPoint(aiTank.consumablePosition, 1f);
            }
            else //No items.
            {
                t += Time.deltaTime;
                if (t > 1)
                {
                    t = 0;
                    return typeof(JRotatingState);
                }
            }
            return null;
        }
    }
}