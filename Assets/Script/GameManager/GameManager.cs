using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JogoPlataforma3D.Singleton;
using Ebac.StateMachine;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates
    {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE,
    }

    public StateMachine<GameStates> stateMachine;
    public PlayerController playerController;

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<GameStates>();

        stateMachine.Init();
        stateMachine.RegisterStates(GameStates.INTRO, new GMStateIntro());
        stateMachine.RegisterStates(GameStates.GAMEPLAY, new StateBase());
        stateMachine.RegisterStates(GameStates.PAUSE, new StateBase());
        stateMachine.RegisterStates(GameStates.WIN, new StateBase());
        stateMachine.RegisterStates(GameStates.LOSE, new StateBase());

        stateMachine.SwitchState(GameStates.INTRO);
    }

    private void Update()
    {
        // Alterna entre INTRO e GAMEPLAY ao pressionar J
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (stateMachine.CurrentState == stateMachine.dictionaryState[GameStates.INTRO])
            {
                stateMachine.SwitchState(GameStates.GAMEPLAY);
            }
            else if (stateMachine.CurrentState == stateMachine.dictionaryState[GameStates.GAMEPLAY])
            {
                stateMachine.SwitchState(GameStates.INTRO);
            }
        }
    }
   
}
