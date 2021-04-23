using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class OLazyTank : RGLMTank
    {
        public float breakTime;
        public float runTime;
        public Vector3 enemyLastPosition;

        public override void AITankStart()
        {
            InitializeStateMachine();
            InitializeRuleBasedSystem();
        }

        public override void AIOnCollisionEnter(Collision collision)
        {
            GetComponent<StateMachine>().SwitchToState(typeof(ORunState));
        }
        public override void AITankUpdate()
        {
            targetTanksFound = GetAllTargetTanksFound;
            consumablesFound = GetAllConsumablesFound;
            basesFound = GetAllBasesFound;
        }

        void InitializeStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states.Add(typeof(OBreakState), new OBreakState(this));
            states.Add(typeof(ORunState), new ORunState(this));
            states.Add(typeof(OMoveState), new OMoveState(this));
            states.Add(typeof(OGatherState), new OGatherState(this));
            GetComponent<StateMachine>().SetStates(states);
        }

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
            stats.Add("targetSpotted", false);
            stats.Add("collectableSpotted", false);


            stats.Add("breakState", true);
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

