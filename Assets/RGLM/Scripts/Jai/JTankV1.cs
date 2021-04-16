using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RGLM
{
    public class JTankV1 : RGLMTank
    {
        [Header("Low Attribute Settings")]
        public float LowHP;
        public float LowAmmo;
        public float LowFuel;


        [Header("Booleans")]
        public bool needsResources = false;
        public bool isRotating = false;

        //State Machine Section
        void InitializeStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states.Add(typeof(JDefaultState), new JDefaultState(this));
            states.Add(typeof(JRotatingState), new JRotatingState(this));
            states.Add(typeof(JSearchingState), new JSearchingState(this));
            states.Add(typeof(JResourceGathering), new JResourceGathering(this));
            states.Add(typeof(JBaseWreakerState), new JBaseWreakerState(this));
            states.Add(typeof(JFleeingState), new JFleeingState(this));
            states.Add(typeof(JGulagState), new JGulagState(this));
            GetComponent<StateMachine>().SetStates(states);
        }

        public override void AITankStart()
        {
            InitializeStateMachine();
        }

        public override void AITankUpdate()
        {
            targetTanksFound = GetAllTargetTanksFound;
            consumablesFound = GetAllConsumablesFound;
            basesFound = GetAllBasesFound;
        }

        public override void AIOnCollisionEnter(Collision collision)
        {
            
        }

        public void Rotate360()
        {
            isRotating = true;
            Calculate360Points();
            StartCoroutine("MoveSightAndWait");
        }

        IEnumerator MoveSightAndWait()
        {
            for (int i = 0; i < 30; i++)
            {
                yield return new WaitForSeconds(0.15f);
                lookAtPosition.transform.position = pointsColection[i];
            }
            isRotating = false;
        }

    }

}