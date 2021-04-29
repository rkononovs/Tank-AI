using System;
using System.Linq;

namespace RGLM
{
    public class RKRotatingState : BaseState
    {
        private BTTank aiTank;

        public RKRotatingState(BTTank aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            // UnityEngine.Debug.LogError("Entered State - Romans Rotating state"); // For DEBUG PURPOSES
            aiTank.Rotate360(); //Call function to turn turret.
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
                return typeof(RKSearchingState); //State change - Searching.
            }
            return null;
        }
    }
}