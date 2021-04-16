using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class JGulagState : BaseState
    {
        private JTankV1 aiTank;

        private float t = 0;

        public JGulagState(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Gulag state");
            return null;
        }

        public override Type StateExit()
        {
            return null;
        }

        public override Type StateUpdate()
        {
            if (aiTank.TankGetFuelLevel() <= aiTank.LowFuel || aiTank.TankGetHealthLevel() <= aiTank.LowHP || aiTank.TankGetFuelLevel() <= aiTank.LowAmmo)
            {
                aiTank.needsResources = true;
                return typeof(JFleeingState);
            }
            else
            { 
                if (aiTank.targetTanksFound.Count > 0 && aiTank.targetTanksFound.First().Key != null)
                {
                    aiTank.targetTankPosition = aiTank.targetTanksFound.First().Key;
                    if (aiTank.targetTankPosition != null)
                    {
                        //get closer to target, and fire
                        if (Vector3.Distance(aiTank.transform.position, aiTank.targetTankPosition.transform.position) < 25f)
                        {
                            aiTank.TankFireAtPoint(aiTank.targetTankPosition);
                        }
                        else
                        {
                            aiTank.TankFollowPathToPoint(aiTank.targetTankPosition, 1f);
                            t += Time.deltaTime;
                            if (t > 10)
                            {
                                t = 0;
                                return typeof(JRotatingState);
                            }
                        }
                    }
                }
                else
                {
                    aiTank.TankFollowPathToPoint(aiTank.targetTankPosition, 1f);
                    if (Vector3.Distance(aiTank.transform.position, aiTank.targetTankPosition.transform.position) < 2f)
                    {
                        return typeof(JFleeingState);
                    }
                }
            }
            return null;
        }
    }
}