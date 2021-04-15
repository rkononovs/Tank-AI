using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
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


        public BTAction healthCheck;
        public BTAction ammoCheck;
        public BTAction targetSpottedCheck;
        public BTAction targetReachedCheck;
        public BTSequence regenSequence;

        public Dictionary<string, bool> stats = new Dictionary<string, bool>();
        public Rules rules = new Rules();


        /*******************************************************************************************************      
        WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
        *******************************************************************************************************/
        public override void AITankStart()
        {
            InitializeStateMachine();
            InitializeRuleBasedSystem();
            InitializeBehaviouralTrees();
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

        //State Machine Section
        void InitializeStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states.Add(typeof(RoamState), new RoamState(this));
            states.Add(typeof(RunState), new RunState(this));
            states.Add(typeof(AnalyzeState), new AnalyzeState(this));
            GetComponent<StateMachine>().SetStates(states);
        }

        //Rule Based Section
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


        // BT Section
        void InitializeBehaviouralTrees()
        {
            healthCheck = new BTAction(HealthCheck);
            ammoCheck = new BTAction(AmmoCheck);
            targetSpottedCheck = new BTAction(TargetSpottedCheck);
            targetReachedCheck = new BTAction(TargetReachedCheck);
            regenSequence = new BTSequence(new List<BTBaseNode> { healthCheck, ammoCheck });
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

        

        //Making use of protected AITank methods
        public new bool IsFiring()
        {
            return IsFiring();
        }

        public new bool IsDestroyed()
        {
            return IsDestroyed();
        }

        public new float GetHealthLevel()
        {
            return GetHealthLevel();
        }

        public new float GetAmmoLevel()
        {
            return GetAmmoLevel();
        }

        public new float GetFuelLevel()
        {
            return GetFuelLevel();
        }

        public new List<GameObject> GetMyBases()
        {
            return GetMyBases();
        }

        public new Dictionary<GameObject, float> GetAllTargetTanksFound()
        {
            return GetAllTargetTanksFound();
        }
        
        public new Dictionary<GameObject, float> GetAllConsumablesFound()
        {
            return GetAllConsumablesFound();
        }

        public new Dictionary<GameObject, float> GetAllBasesFound()
        {
            return GetAllBasesFound();
        }

        public new void FindPathToPoint(GameObject pointInWorld)
        {
            FindPathToPoint(pointInWorld);
        }
        public new void FollowPathToPoint(GameObject pointInWorld, float normalizedSpeed)
        {
            FollowPathToPoint(pointInWorld,normalizedSpeed);
        }

        public new void FollowPathToRandomPoint(float normalizedSpeed)
        {
            FollowPathToRandomPoint(normalizedSpeed);
        }

        public new void GenerateRandomPoint()
        {
            GenerateRandomPoint();
        }

        public new void StopTank()
        {
            StopTank();
        }

        public new void StartTank()
        {
            StartTank();
        }

        public new void FaceTurretToPoint(Vector3 pointInWorld)
        {
            FaceTurretToPoint(pointInWorld);
        }

       public new void ResetTurret()
        {
            ResetTurret();
        }

        public new void FireAtPoint(GameObject pointInWorld)
        {
            FireAtPoint(pointInWorld);
        }


    }
}

