using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace RGLM
{
    public class BTTank : AITank
    {
        public BTAction healthCheck;
        public BTAction ammoCheck;
        public BTAction targetSpottedCheck;
        public BTAction targetReachedCheck;
        public BTSequence regenSequence;

        /*******************************************************************************************************      
        WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
        *******************************************************************************************************/
        public override void AITankStart()
        {
            healthCheck = new BTAction(HealthCheck);
            ammoCheck = new BTAction(AmmoCheck);
            targetSpottedCheck = new BTAction(TargetSpottedCheck);
            targetReachedCheck = new BTAction(TargetReachedCheck);
            regenSequence = new BTSequence(new List<BTBaseNode> {healthCheck, ammoCheck });
        }

        public BTNodeStates TargetReachedCheck()
        {
            throw new NotImplementedException();
        }

        public BTNodeStates TargetSpottedCheck()
        {
            throw new NotImplementedException();
        }

        public BTNodeStates AmmoCheck()
        {
            throw new NotImplementedException();
        }

        public BTNodeStates HealthCheck()
        {
            throw new NotImplementedException();
        }

        /*******************************************************************************************************       
        WARNING, do not include void Update(), use AITankUpdate() instead if you want to use Update method from Monobehaviour.
        *******************************************************************************************************/
        public override void AITankUpdate()
        {
        }

        /*******************************************************************************************************       
        WARNING, do not include void OnCollisionEnter(), use AIOnCollisionEnter() instead if you want to use Update method from Monobehaviour.
        *******************************************************************************************************/
        public override void AIOnCollisionEnter(Collision collision)
        {
        }
    }
}

