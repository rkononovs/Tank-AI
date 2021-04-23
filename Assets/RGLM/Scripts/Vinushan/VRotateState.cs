using System;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class VRotateState : BaseState
    {
        private VTankV1 aiTank;

        public VRotateState(VTankV1 aiTank)
        {
            this.aiTank = aiTank;
        }

        public override Type StateEnter()
        {
            Debug.Log("Rotate State - Rotate 360 degrees");
            //aiTank.TankFollowPathToRandomPoint(1); // Call function to go to random point
            aiTank.Rotate360(); // Call function to turn turret.
            return null;
        }

        public override Type StateExit()
        {
            return null;
        }

        public override Type StateUpdate()
        {
            aiTank.TankFaceTurretToPoint(aiTank.lookAtPosition.transform.position); // Point turret to rotate 360 degrees

            return typeof(VSearchingState); // Searching state
            
        }
    }
}