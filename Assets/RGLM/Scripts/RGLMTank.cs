﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

namespace RGLM
{
    public class RGLMTank : AITank
    {
        public Dictionary<GameObject, float> targetTanksFound = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> consumablesFound = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> basesFound = new Dictionary<GameObject, float>();

        public GameObject targetTankPosition;
        public GameObject consumablePosition;
        public GameObject basePosition;

        float t;

        bool lowHealth;

        public Dictionary<string, bool> stats = new Dictionary<string, bool>();
        public Rules rules = new Rules();


        /*******************************************************************************************************      
        WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
        *******************************************************************************************************/
        public override void AITankStart()
        {
            InitializeStateMachine();
            InitializeRuleBasedSystem();
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

        void InitializeStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states.Add(typeof(RoamState), new RoamState(this));
            states.Add(typeof(RunState), new RunState(this));
            states.Add(typeof(AnalyzeState), new AnalyzeState(this));
            GetComponent<StateMachine>().SetStates(states);
        }

        void InitializeRuleBasedSystem()
        {
            stats.Add("lowHealth", lowHealth);
            stats.Add("targetSpotted", false);
            stats.Add("targetReached", false);
            stats.Add("fleeState", false);
            stats.Add("searchState", false);
            stats.Add("attackState", false);

            rules.AddRule(new Rule("attackState", "lowHealth", typeof(RoamState), Rule.Predicate.And));
            rules.AddRule(new Rule("attackState", "lowHealth", typeof(RoamState), Rule.Predicate.And));

        }
    }
}

