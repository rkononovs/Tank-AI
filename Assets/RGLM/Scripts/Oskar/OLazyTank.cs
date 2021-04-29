using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class OLazyTank : RGLMTank
    {
        public Vector3 enemyLastPosition;

        [Header("Tank settings")]
        public float breakTime;
        public float runTime;

        [Header("Attributes ")]
        public float HP;
        public float Ammo;
        public float Fuel;

        //Initialize everything
        public override void AITankStart()
        {
            InitializeStateMachine();
            InitializeRuleBasedSystem();
        }

        //If the tank got shot then go to the running away  state straight away
        public override void AIOnCollisionEnter(Collision collision)
        {
            GetComponent<StateMachine>().SwitchToState(typeof(ORunState));
        }

        //Update all parameters
        public override void AITankUpdate()
        {
            targetTanksFound = GetAllTargetTanksFound;
            consumablesFound = GetAllConsumablesFound;
            basesFound = GetAllBasesFound;

            //For testing purposes
            HP = TankGetHealthLevel();
            Ammo = TankGetAmmoLevel();
            Fuel = TankGetFuelLevel();
        }

        void InitializeStateMachine()
        {
            //Assign all states to the state machine
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states.Add(typeof(OBreakState), new OBreakState(this));
            states.Add(typeof(ORunState), new ORunState(this));
            states.Add(typeof(OMoveState), new OMoveState(this));
            states.Add(typeof(OGatherState), new OGatherState(this));
            GetComponent<StateMachine>().SetStates(states);
        }

        //Detecting a resource
        public void IsCollectableNearby()
        {
            if (consumablesFound.Count > 0)
            {
                consumablePosition = consumablesFound.First().Key;
                stats["collectableSpotted"] = true;
            }
            else
            {
                consumablePosition = null;
                stats["collectableSpotted"] = false;
            }
        }

        //Detecting an enemy
        public void IsEnemyNearby()
        {
            if (targetTanksFound.Count > 0)
            {
                enemyLastPosition = targetTanksFound.First().Key.transform.position;
                stats["targetSpotted"] = true;
            }
            else
            {
                stats["targetSpotted"] = false;
            }
        }

        void InitializeRuleBasedSystem()
        {
            //Setting up some conditions
            stats.Add("targetSpotted", false);
            stats.Add("collectableSpotted", false);

            stats.Add("breakState", true); //Start in breaking state 
            stats.Add("movingState", false);
            stats.Add("runState", false);
            stats.Add("gatherState", false);

            //Prioritze resources
            rules.AddRule(new Rule("collectableSpotted", "breakState", typeof(OGatherState), Rule.Predicate.And));
            rules.AddRule(new Rule("collectableSpotted", "movingState", typeof(OGatherState), Rule.Predicate.And));
            rules.AddRule(new Rule("collectableSpotted", "runState", typeof(OGatherState), Rule.Predicate.And));

            //Run if you see an enemy
            rules.AddRule(new Rule("targetSpotted", "breakState", typeof(ORunState), Rule.Predicate.And));
            rules.AddRule(new Rule("targetSpotted", "movingState", typeof(ORunState), Rule.Predicate.And));

        }
    }
}

