using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;

namespace Boss
{
    public class BossStateBase : StateBase
    {
        protected BossBase boss;

        public override void OnStateEnter (params object[] objs)
        {
            base.OnStateEnter (objs);
            boss = (BossBase)objs[0];
        }
    }

    public class BossStateInit : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartInitAnimation();

            Debug.Log("Boss: " + boss);
        }
    }

    public class BossStateWalk : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.GoToRandomPoint(OnArrive);

            Debug.Log("Boss: " + boss);
        }

        private void OnArrive()
        {
            boss.SwitchState(BossAction.ATTACK);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
            Debug.Log("Exit Walk");
        }

    }

    public class BossStateAttack : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.StartAttack(EndAttacks);
        }

        private void EndAttacks()
        {
            boss.SwitchState(BossAction.WALK);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            boss.StopAllCoroutines();
            Debug.Log("Exit Attack");
        }
    }

    public class BossStateDeath : BossStateBase
    {
        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            boss.DeathAnimation();
            Debug.Log("Enter Death");
        }
    }
}
