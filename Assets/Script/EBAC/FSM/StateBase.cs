using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(object o = null)
        {
            Debug.Log("OnStateEnter");
        }

        public virtual void OnStateStay()
        {
            Debug.Log("OnStateStay");
        }

        public virtual void OnStateExit()
        {
            Debug.Log("OnStateExit");
        }
    }

    public class StateJump : StateBase
    {
        public PlayerController playerController;

        public override void OnStateEnter(object o = null)
        {
            playerController = (PlayerController)o;
            playerController.canJump = false;
            playerController.canMove = false;

            base.OnStateEnter(o);
        }

        public override void OnStateExit()
        {
            playerController.canMove = true;
            playerController.canJump = true;

            base.OnStateExit();
        }
    }
}
