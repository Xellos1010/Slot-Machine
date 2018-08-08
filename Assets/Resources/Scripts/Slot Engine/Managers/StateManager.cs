//
//
//  Generated by StarUML(tm) C# Add-In
//
//  @ Project : Slot Engine
//  @ File Name : StateManager.cs
//  @ Date : 5/7/2014
//  @ Author : Evan McCall
//
//

public static class StateManager
{
    public static States enCurrentState;
    public static States enCurrentMode
    {
        get
        {
            if (enCurrentState == States.BaseGameRacking ||
                enCurrentState == States.BaseGameSpinEnd ||
                enCurrentState == States.BaseGameSpinLoop ||
                enCurrentState == States.BaseGameSpinStart ||
                enCurrentState == States.BaseGameWinPresentation
                )
                return States.BaseGame;
            else
                return States.BonusGame;
        }
    }

    //State Switching Variables
    public delegate void StateDelegate(States State);
    public static event StateDelegate ActivateSwitchState;
    public static event StateDelegate StateSwitched;
    //*************

    //Unity Functions

    //*********

    //State Manager Functions
	public static void SwitchState(States State)
    {
        enCurrentState = State;
        ActivateSwitchState(State);
	}

    public static void SwitchStateSpin(States SlotEngineState)
    {
        if (SlotEngineState == States.BaseGameSpinStart)
            SwitchState(States.BaseGameSpinStart);
        else if (SlotEngineState == States.BonusGameSpinStart)
            SwitchState(States.BonusGameSpinStart);
    }
}
