using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
   
    public enum CurrentPlayerState
    {
        Alive,
        Dead
    }

    public enum Scene
    {
        UnKnown,
        Login,
        Lobby,
        Game
    }

    public enum Sound
    {
        BGM,
        Effect,
        MaxCount
    }

    public enum UIEvent
    {
        Click,
        Drag
    }

    public enum MouseEvent
    {
        Click,
        Press
    }

    public enum CameraMode
    {
        BigShock,
        MediumShock,
        SmallShock
    }

    public enum CircleType
    {

        Basic,
        Triangle,
        DoubleTriAngle
    }


}
