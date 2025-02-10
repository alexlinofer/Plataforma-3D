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

    /*public class StateJump : StateBase
    {
        public  PlayerController playerController;

        public override void OnStateEnter(object o = null)
        {
            playerController = (PlayerController)o;
            playerController.canMove = false;
            playerController.canJump = false;

            Debug.Log("Entering JUMP State");
            base.OnStateEnter(o);
        }

        public override void OnStateExit()
        {
            playerController.canMove = true;
            playerController.canJump = true;

            Debug.Log("Exiting JUMP State");
            base.OnStateExit();
        }
    }


    public class StateIntro : StateBase
    {
        public PlayerController playerController;
        public override void OnStateEnter(object o = null)
        {
            playerController = (PlayerController)o;
            playerController.canMove = false;
            playerController.canJump = false;
            playerController.canPerformActions = false;

            Debug.Log("Entering INTRO State");
            base.OnStateEnter(o);
        }

        public override void OnStateExit()
        {
            // Quando saímos do estado INTRO, habilitamos as ações
            playerController.canMove = true;
            playerController.canPerformActions = true;
            playerController.canJump = true;

            Debug.Log("Exiting INTRO State");
            base.OnStateExit();

            if (playerController == null)
            {
                Debug.LogError("PlayerController está nulo ao sair do estado!");
                return;
            }

        }
    }*/


}
