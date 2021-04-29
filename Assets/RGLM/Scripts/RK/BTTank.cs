using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

namespace RGLM
{
    public class BTTank : RGLMTank
    {
        GameObject consumable;

        // Headers for low attribute
        [Header("Low Attribute Settings")]
        public float LowHP;
        public float LowAmmo;
        public float LowFuel;

        // Headers for attributes
        [Header("Attributes")]
        public float HP;
        public float Ammo;
        public float Fuel;
        public float waitTime;

        // Header for booleans we use
        [Header("Booleans")]
        public bool needsResources = false;
        public bool isRotating = false;

        // Behaviour tree actions
        public BTAction enemyCheck;
        public BTAction fireTurret;
        public BTAction rotateTurret;

        // Behaviour tree sequences
        public BTSequence fireAtEnemy;

        /*******************************************************************************************************      
        WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
        *******************************************************************************************************/
        public override void AITankStart()
        {
            InitializeStateMachine();
            InitializeBehaviouralTrees();
        }

        /*******************************************************************************************************       
        WARNING, do not include void Update(), use AITankUpdate() instead if you want to use Update method from Monobehaviour.
        *******************************************************************************************************/
        public override void AITankUpdate()
        {
            targetTanksFound = GetAllTargetTanksFound; // Get all tanks we found
            consumablesFound = GetAllConsumablesFound; // Get all consumables we found
            basesFound = GetAllBasesFound; // Get all bases we found

            HP = TankGetHealthLevel(); // Get current hp level
            Ammo = TankGetAmmoLevel(); // Get current ammo level
            Fuel = TankGetFuelLevel(); // Get current fuel level
        }

        /*******************************************************************************************************       
        WARNING, do not include void OnCollisionEnter(), use AIOnCollisionEnter() instead if you want to use Update method from Monobehaviour.
        *******************************************************************************************************/
        public override void AIOnCollisionEnter(Collision collision)
        {
        }

        public void Rotate360() //Call method for turret rotation.
        {
            //Debug.Log("Rotating turret 1"); // For DEBUG PURPOSES ONLY
            isRotating = true; // Set rotating as true
            Calculate360Points(); // Calculate points so we can look at them and rotate turret
            StartCoroutine("MoveSightAndWait");
            //Debug.Log("Rotating turret 2"); // For DEBUG PURPOSES ONLY
        }

        IEnumerator MoveSightAndWait() //Turn turret.
        {
            for (int i = 0; i < 30; i++)  // Look at the points
            {
                yield return new WaitForSeconds(0.15f);
                lookAtPosition.transform.position = pointsColection[i];
                //Debug.Log("Rotating turret"); // For DEBUG PURPOSES ONLY
            }
            isRotating = false; // Set rotating as false
        }

        //State Machine Section
        void InitializeStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>(); // Create dictiory with states

            // Initialize all states
            states.Add(typeof(RKWaitRotateState), new RKWaitRotateState(this));
            states.Add(typeof(RKRotatingState), new RKRotatingState(this));
            states.Add(typeof(RKResourceGathering), new RKResourceGathering(this));
            states.Add(typeof(RKGulagState), new RKGulagState(this));
            states.Add(typeof(RKFleeingState), new RKFleeingState(this));
            states.Add(typeof(RKBaseWreakerState), new RKBaseWreakerState(this));
            states.Add(typeof(RKAttack), new RKAttack(this));
            states.Add(typeof(RKSearchingState), new RKSearchingState(this));

            GetComponent<StateMachine>().SetStates(states); // Set base state
        }

        void InitializeBehaviouralTrees() // Initialize BT
        {
            // Create new BT actions
            enemyCheck = new BTAction(EnemyCheck);
            fireTurret = new BTAction(FireTurret);
            rotateTurret = new BTAction(RotateTurret);

            // Make a new sequence from actions
            fireAtEnemy = new BTSequence(new List<BTBaseNode> {enemyCheck, rotateTurret, fireTurret });
        }

        public BTNodeStates FireTurret()
        {
            GameObject enemy = targetTanksFound.First().Key; // Get enemy gameobject
            FireAtPoint(enemy); // Fire at the enemy gameobject position
            return BTNodeStates.SUCCESS; // Return successfull action
        }

        public BTNodeStates RotateTurret()
        {
            GameObject enemy = targetTanksFound.First().Key; // Get enemy gameobject
            FaceTurretToPoint(enemy.transform.position); // Rotate turret to the enemy gameobject position
            return BTNodeStates.SUCCESS; // Return successfull action
        }

        public BTNodeStates EnemyCheck()
        {
            if (targetTanksFound.Count > 0 && targetTanksFound.First().Key != null) // If tanks are found return success
            {
                return BTNodeStates.SUCCESS;
            }
            else // Else return false
            {
                return BTNodeStates.FAILURE; 
            }
        }

        //Making use of protected AITank methods
        public bool TankIsFiring()
        {
            return IsFiring;
        }

        public bool TankIsDestroyed()
        {
            return IsDestroyed;
        }

        public float TankGetHealthLevel()
        {
            return GetHealthLevel;
        }

        public float TankGetAmmoLevel()
        {
            return GetAmmoLevel;
        }

        public float TankGetFuelLevel()
        {
            return GetFuelLevel;
        }

        public List<GameObject> TankGetMyBases()
        {
            return GetMyBases;
        }

        public Dictionary<GameObject, float> TankGetAllTargetTanksFound()
        {
            return GetAllTargetTanksFound;
        }

        public Dictionary<GameObject, float> TankGetAllConsumablesFound()
        {
            return GetAllConsumablesFound;
        }

        public Dictionary<GameObject, float> TankGetAllBasesFound()
        {
            return GetAllBasesFound;
        }

        public void TankFindPathToPoint(GameObject pointInWorld)
        {
            FindPathToPoint(pointInWorld);
        }
        public void TankFollowPathToPoint(GameObject pointInWorld, float normalizedSpeed)
        {
            FollowPathToPoint(pointInWorld, normalizedSpeed);
        }

        public void TankFollowPathToRandomPoint(float normalizedSpeed)
        {
            FollowPathToRandomPoint(normalizedSpeed);
        }

        public void TankGenerateRandomPoint()
        {
            GenerateRandomPoint();
        }

        public void TankStopTank()
        {
            StopTank();
        }

        public void TankStartTank()
        {
            StartTank();
        }

        public void TankFaceTurretToPoint(Vector3 pointInWorld)
        {
            FaceTurretToPoint(pointInWorld);
        }

        public void TankResetTurret()
        {
            ResetTurret();
        }

        public void TankFireAtPoint(GameObject pointInWorld)
        {
            FireAtPoint(pointInWorld);
        }
    }
}

