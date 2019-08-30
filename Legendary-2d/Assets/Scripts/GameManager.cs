using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PLAYING,
    PAUSE, 
    WIN,
    LOSE
}

public class GameManager : Singleton<GameManager>
{
    public GameState state;

    private CharacterManager character;
    public CharacterManager Character
    {
        get
        {
            if(character == null)
            {
                character = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterManager>();
            }
            return character;
        }
        private set
        {
            character = value;
        }
    }

    [Range(0, 1)]
    public float TIME_STEP_X_AXIS;

    public float DISTANCE_COL { get; set; }

    public float MIN_X;
    public float MAX_X;

}

