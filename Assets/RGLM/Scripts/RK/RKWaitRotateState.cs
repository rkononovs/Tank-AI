using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RGLM
{
    public class RKWaitRotateState : BaseState
    {
        float t = 0;
        private BTTank aiTank;

        public RKWaitRotateState(BTTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            return null;
        }

        public override Type StateExit()
        {
            return null;
        }

        public override Type StateUpdate()
        {
            aiTank.TankFaceTurretToPoint(aiTank.lookAtPosition.transform.position); //Update the direction turret is facing.
            if (aiTank.consumablesFound.Count > 0) //If consumable found.
            {
                return typeof(RKResourceGathering); //State change - Resource gathering.
            }
            if (aiTank.targetTanksFound.Count > 0) //If enemy found.
            {
                if (aiTank.needsResources) //If low on resources.
                {
                    return typeof(RKFleeingState); //State change - Fleeing.
                }
                else
                {
                    return typeof(RKGulagState); //State change - Gulag.
                }
            }
            if (!aiTank.isRotating) //If finished rotating turret.
            {
                t += Time.deltaTime;
                if (t > 5) // Rotate turret once in five seconds
                {
                    t = 0;
                    aiTank.Rotate360(); //Call function to turn turret.
                }
            }
            return null;
        }
    }
}