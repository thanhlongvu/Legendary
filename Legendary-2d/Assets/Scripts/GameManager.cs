using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PLAY,
    PAUSE
}

public class GameManager : Singleton<GameManager>
{
    public GameState state; 
}

