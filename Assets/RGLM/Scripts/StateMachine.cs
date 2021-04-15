using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RGLM
{
    public class StateMachine : AITank
    {
        private Dictionary<Type, BaseState> states;
        private BaseState currentState;

        public BaseState CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                currentState = value;
            }
        }

        public void SetStates(Dictionary<Type, BaseState> states)
        {
            this.states = states;
        }

        public override void AIOnCollisionEnter(Collision collision)
        {
        }

        public override void AITankUpdate()
        {
            if(CurrentState == null)
            {
                CurrentState = states.Values.First();
            }
            else
            {
                var nextState = CurrentState.StateUpdate();

                if(nextState != null && nextState != CurrentState.GetType())
                {
                    SwitchToState(nextState);
                }
            }
        }

        void SwitchToState(Type nextState)
        {
            CurrentState.StateExit();
            CurrentState = states[nextState];
            CurrentState.StateEnter();
        }

        public override void AITankStart()
        {
        }

    }
}

