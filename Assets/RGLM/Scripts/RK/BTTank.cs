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
        [Header("Low Attribute Settings")]
        public float LowHP;
        public float LowAmmo;
        public float LowFuel;

        [Header("Attributes")]
        public float HP;
        public float Ammo;
        public float Fuel;
        public float waitTime;

        [Header("Booleans")]
        public bool needsResources = false;
        public bool isRotating = false;

        public BTAction fuelCheck;
        public BTAction enemyCheck;
        public BTSequence fleeSequence;

        /*
public Dictionary<GameObject, float> targetTanksFound = new Dictionary<GameObject, float>();
public Dictionary<GameObject, float> consumablesFound = new Dictionary<GameObject, float>();
public Dictionary<GameObject, float> basesFound = new Dictionary<GameObject, float>();

public GameObject targetTankPosition;
public GameObject consumablePosition;
public GameObject basePosition;

public BTAction healthCheck;
public BTAction ammoCheck;
public BTAction targetSpottedCheck;
public BTAction targetReachedCheck;
public BTSequence regenSequence;
*/

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
            targetTanksFound = GetAllTargetTanksFound;
            consumablesFound = GetAllConsumablesFound;
            basesFound = GetAllBasesFound;

            HP = TankGetHealthLevel();
            Ammo = TankGetAmmoLevel();
            Fuel = TankGetFuelLevel();
        }

        /*******************************************************************************************************       
        WARNING, do not include void OnCollisionEnter(), use AIOnCollisionEnter() instead if you want to use Update method from Monobehaviour.
        *******************************************************************************************************/
        public override void AIOnCollisionEnter(Collision collision)
        {
        }

        public void Rotate360() //Call method for turret rotation.
        {
            Debug.Log("ROtating turret1");
            isRotating = true;
            Calculate360Points();
            StartCoroutine("MoveSightAndWait");
            Debug.Log("ROtating turret2");
        }

        IEnumerator MoveSightAndWait() //Turn turret.
        {
            for (int i = 0; i < 30; i++)
            {
                yield return new WaitForSeconds(0.15f);
                lookAtPosition.transform.position = pointsColection[i];
                Debug.Log("ROtating turret");
            }
            isRotating = false;
        }

        //State Machine Section
        void InitializeStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states.Add(typeof(RKWaitRotateState), new RKWaitRotateState(this));
            states.Add(typeof(RKRotatingState), new RKRotatingState(this));
            states.Add(typeof(RKResourceGathering), new RKResourceGathering(this));
            states.Add(typeof(RKGulagState), new RKGulagState(this));
            states.Add(typeof(RKFleeingState), new RKFleeingState(this));
            states.Add(typeof(RKBaseWreakerState), new RKBaseWreakerState(this));
            states.Add(typeof(RKAttack), new RKAttack(this));
            GetComponent<StateMachine>().SetStates(states);
        }

        void InitializeBehaviouralTrees()
        {
            healthCheck = new BTAction(HealthCheck);
            ammoCheck = new BTAction(AmmoCheck);
            fuelCheck = new BTAction(FuelCheck);
            enemyCheck = new BTAction(EnemyCheck);
            fleeSequence = new BTSequence(new List<BTBaseNode> { });
        }

        public BTNodeStates AmmoCheck()
        {
            if(TankGetAmmoLevel() > LowAmmo)
            {
                return BTNodeStates.FAILURE;
            }
            else
            {
                return BTNodeStates.SUCCESS;
            }
        }

        public BTNodeStates HealthCheck()
        {
            if(TankGetHealthLevel() > LowHP)
            {
                return BTNodeStates.FAILURE;
            }
            else
            {
                return BTNodeStates.SUCCESS;
            }
        }

        public BTNodeStates FuelCheck()
        {
            if (TankGetHealthLevel() > LowFuel)
            {
                return BTNodeStates.FAILURE;
            }
            else
            {
                return BTNodeStates.SUCCESS;
            }
        }

        public BTNodeStates EnemyCheck()
        {
            if (targetTanksFound.Count > 0 && targetTanksFound.First().Key != null)
            {
                return BTNodeStates.FAILURE;
            }
            else
            {
                return BTNodeStates.SUCCESS;
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

