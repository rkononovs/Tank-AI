using System;
using System.Collections.Generic;
using UnityEngine;


namespace RGLM
{
    public class VTankV1 : RGLMTank
    {
        [Header("Attributes")]
        public float HP;
        public float Ammo;
        public float Fuel;

        //State Machine Section
        void InitializeStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states.Add(typeof(VRunState), new VRunState(this));
            states.Add(typeof(VGatheringState), new VGatheringState(this));
            states.Add(typeof(VSearchingState), new VSearchingState(this));
            states.Add(typeof(VFleeingState), new VFleeingState(this));
            GetComponent<StateMachine>().SetStates(states);
        }

        void InitializeRuleBasedSystem()
        {
            stats.Add("ResourceNear", false);
            stats.Add("EnemyNear", false);
            stats.Add("RunState", false);
            stats.Add("GatheringState", false);
            stats.Add("SearchingState", false);
            stats.Add("FleeingState", false);

            rules.AddRule(new Rule("ResourceNear", "RunState", typeof(VGatheringState), Rule.Predicate.And));
            rules.AddRule(new Rule("ResourceNear", "SearchingState", typeof(VGatheringState), Rule.Predicate.And));
            rules.AddRule(new Rule("EnemyNear", "RunState", typeof(VFleeingState), Rule.Predicate.And));
            rules.AddRule(new Rule("EnemyNear", "GatheringState", typeof(VFleeingState), Rule.Predicate.And));
            rules.AddRule(new Rule("EnemyNear", "SearchingState", typeof(VFleeingState), Rule.Predicate.And));
        }

        public override void AITankStart()
        {
            InitializeStateMachine();
            InitializeRuleBasedSystem();
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

        public void ResourceNear()
        {
            if (consumablesFound.Count > 0)
            {
                stats["ResourceNear"] = true;
            }
            else
            {
                stats["ResourceNear"] = false;
            }
        }

        public void EnemyNear()
        {
            if (targetTanksFound.Count > 0)
            {
                stats["EnemyNear"] = true;
            }
            else
            {
                stats["EnemyNear"] = false;
            }
        }

    }

}