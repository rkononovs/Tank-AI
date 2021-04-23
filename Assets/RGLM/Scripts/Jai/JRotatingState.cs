using System;
using System.Linq;

namespace RGLM
{
    public class JRotatingState : BaseState
    {
        private JTankV1 aiTank;

        public JRotatingState(JTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            UnityEngine.Debug.LogError("Entered State - Jai Rotating state");
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
                return typeof(JResourceGathering); //State change - Resource gathering.
            }
            if (aiTank.targetTanksFound.Count > 0) //If enemy found.
            {
                if (aiTank.needsResources) //If low on resources.
                {
                    return typeof(JFleeingState); //State change - Fleeing.
                }
                else
                {
                    return typeof(JGulagState); //State change - Gulag.
                }
            }
            if (!aiTank.isRotating) //If finished rotating turret.
            {
                return typeof(JSearchingState); //State change - Searching.
            }
            return null;
        }
    }
}