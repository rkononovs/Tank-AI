using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RGLM
{
    public class JTankV1 : RGLMTank
    {
        //Attributes to tweak in unity
        [Header("Low Attribute Settings")]
        public float LowHP;
        public float LowAmmo;
        public float LowFuel;
        //Display info about tank in unity
        [Header("Attributes")]
        public float HP;
        public float Ammo;
        public float Fuel;

        [Header("Booleans")]
        public bool needsResources = false;
        public bool isRotating = false;

        //State Machine Section
        void InitializeStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states.Add(typeof(JDefaultState), new JDefaultState(this));//Used so that rotating state can call functions on state enter
            states.Add(typeof(JRotatingState), new JRotatingState(this));//Rotates the turret
            states.Add(typeof(JSearchingState), new JSearchingState(this));//Search for enemy tank and resources
            states.Add(typeof(JResourceGathering), new JResourceGathering(this));//Move to the resource
            states.Add(typeof(JBaseWreakerState), new JBaseWreakerState(this));//Attack enemy base
            states.Add(typeof(JFleeingState), new JFleeingState(this));//Run while low on resources
            states.Add(typeof(JGulagState), new JGulagState(this));//Attack enemy tank
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
            HP = TankGetHealthLevel();
            Ammo = TankGetAmmoLevel();
            Fuel = TankGetFuelLevel();
        }

        public override void AIOnCollisionEnter(Collision collision)
        {
            
        }

        public void Rotate360() //Method for turret rotation.
        {
            isRotating = true;
            Calculate360Points();
            StartCoroutine("MoveSightAndWait");
        }

        IEnumerator MoveSightAndWait() //Turn turret.
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