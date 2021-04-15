using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace RGLM
{
    public class BTTank : AITank
    {
        /*******************************************************************************************************      
        WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
        *******************************************************************************************************/
        public override void AITankStart()
        {
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

