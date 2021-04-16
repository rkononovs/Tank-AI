using System;
using System.Diagnostics;
using UnityEngine;

namespace RGLM
{
    public class JDefaultState : BaseState
    {
        private JTankV1 aiTank;

        public JDefaultState(JTankV1 aiTank)
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
            return typeof(JRotatingState); //State change - Rotating.
        }
    }
}
