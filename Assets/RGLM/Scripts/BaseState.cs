
using System;
using System.Linq;
namespace RGLM
{
    public abstract class BaseState
    {
        public abstract Type StateUpdate();
        public abstract Type StateEnter();
        public abstract Type StateExit();
    }
}

