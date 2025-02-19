using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using DG.Tweening;
using UnityEditor;
using Animation;
using TMPro;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        private Player _player; // adicionado por mim

        [Header("Animation")]
        public float startAnimationDuration = 1f;
        public Ease startAnimationEase = Ease.OutBack;
        [SerializeField] private AnimationBase _animationBase;
        public GameObject bossGraphic;
        private bool _hasInit = false;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;
        public GunBase gunBase;

        public float speed = 5f;
        public List<Transform> waypoints;

        public HealthBase healthBase;

        private StateMachine<BossAction> stateMachine;

        private void Awake()
        {
            Init();
            healthBase.OnKill += OnBossKill;
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if (p != null)
            {
                p.Damage(1);

                Debug.Log("Collision");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_hasInit)
            {
                SwitchState(BossAction.INIT);
                _hasInit = true;
            }
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #region DEATH
        public void DeathAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(Ease.Linear);
            PlayAnimationByTrigger(AnimationType.DEATH);
        }
        #endregion

        #region ATTACK
        public void StartAttack(Action endCallback = null)
        {
            if (healthBase.destroyOnKill) return; // adicionado por mim
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback = null)
        {
            int attacks = 0;
            while(attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                
                Vector3 targetPosition = _player.transform.position;
                targetPosition.y -= 2f;
                transform.LookAt(targetPosition);

                gunBase.Shoot(); // adicionado por mim

                PlayAnimationByTrigger(AnimationType.ATTACK);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallback?.Invoke();
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            if (healthBase.destroyOnKill) return; // adicionado por mim
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0, waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while(Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                transform.LookAt(t);
                PlayAnimationByTrigger(AnimationType.RUN);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();

        }


        #endregion

        #region INIT
        public void StartInitAnimation()
        {
            StartCoroutine(InitCoroutine()); // adicionado por mim
        }

        // corotina adicionada por mim
        IEnumerator InitCoroutine()
        {
            bossGraphic.SetActive(true);
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
            yield return new WaitForSeconds(startAnimationDuration);
            SwitchState(BossAction.WALK);
        }


        #endregion

        #region DEBUG

        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }

        [NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }

        [NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }

        [NaughtyAttributes.Button]
        private void SwitchDeath()
        {
            SwitchState(BossAction.DEATH);
        }

        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion
    }
}

