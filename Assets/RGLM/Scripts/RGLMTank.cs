using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The main script that is the base for all scripts

namespace RGLM
{
    public class RGLMTank : AITank
    {
        //Things the tank sees
        public Dictionary<GameObject, float> targetTanksFound = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> consumablesFound = new Dictionary<GameObject, float>();
        public Dictionary<GameObject, float> basesFound = new Dictionary<GameObject, float>();

        //The position of these things
        public GameObject targetTankPosition;
        public GameObject consumablePosition;
        public GameObject basePosition;

        //For rotation and aiming
        public GameObject lookAtPosition;
        public Vector3[] pointsColection = new Vector3[30]; //points around the tank needed for rotating

        //Rule based system
        public Dictionary<string, bool> stats = new Dictionary<string, bool>();
        public Rules rules = new Rules();


        /*******************************************************************************************************      
        WARNING, do not include void Start(), use AITankStart() instead if you want to use Start method from Monobehaviour.
        *******************************************************************************************************/
        //Initialize everything
        public override void AITankStart()
        {
            InitializeStateMachine();
            InitializeRuleBasedSystem();
            InitializeBehaviouralTrees();
        }

        /*******************************************************************************************************       
        WARNING, do not include void Update(), use AITankUpdate() instead if you want to use Update method from Monobehaviour.
        *******************************************************************************************************/

        //Get data from sensors
        public override void AITankUpdate()
        {
            targetTanksFound = GetAllTargetTanksFound;
            consumablesFound = GetAllConsumablesFound;
            basesFound = GetAllBasesFound;
        }

        /*******************************************************************************************************       
        WARNING, do not include void OnCollisionEnter(), use AIOnCollisionEnter() instead if you want to use Update method from Monobehaviour.
        *******************************************************************************************************/
        public override void AIOnCollisionEnter(Collision collision)
        {
        }


        private void Awake()
        {
            SpawnLookPoint(); //Spawning an empty gameobject to point turret in certain direction
        }

        //New methods section
        void SpawnLookPoint()
        {
            if (lookAtPosition == null)
            {
                lookAtPosition = new GameObject("RGLM look position");
            }
        }
        public void Calculate360Points() //Points for turret to rotate 360 degrees.
        {
            float nextAngle = -77;
            float temp = 360 / 30 ;

            for (int i = 0; i<30; i++)
            {
                float x = transform.position.x + Mathf.Cos(nextAngle * ((float)Math.PI / 180)) * 50;
                float z = transform.position.x + Mathf.Sin(nextAngle * ((float)Math.PI / 180)) * 50;
                float y = transform.position.y;
                pointsColection[i] = new Vector3(x, y, z);
                nextAngle += temp;
            }

        }

        public void Rotate360() //360 turret rotation.
        {
            Calculate360Points();
            StartCoroutine("MoveSightAndWait");
        }

        IEnumerator MoveSightAndWait() //Independent turret movement around the tank
        {
            for(int i = 0; i<30; i++)
            {
                yield return new WaitForSeconds(0.15f);
                lookAtPosition.transform.position = pointsColection[i];
            }
        }


        //Placeholder for State Machine 
        void InitializeStateMachine()
        {
        }

        //Placeholder for Rule Based System 
        void InitializeRuleBasedSystem()
        {
        }


        // Placeholder for BTs
        void InitializeBehaviouralTrees()
        {
        }
      
        //Rewriting protected methods with "Tank%" prefix to be able to use them. A
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

