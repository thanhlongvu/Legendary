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
    [SerializeField]
    private float distanceCol;
    public float DistanceCol {
        get
        {
            return distanceCol;
        }
    }
}

