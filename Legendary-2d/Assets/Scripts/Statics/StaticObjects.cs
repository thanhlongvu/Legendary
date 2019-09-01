using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjects
{
    public readonly static WaitForSeconds WAIT_TIME_ZERO_POINT_ONE = new WaitForSeconds(0.1f);
    public readonly static WaitForSeconds WAIT_TIME_QUARTER = new WaitForSeconds(0.25f);
    public readonly static WaitForSeconds WAIT_TIME_HAFT = new WaitForSeconds(0.5f);
    public readonly static WaitForSeconds WAIT_TIME_ONE = new WaitForSeconds(1f);
    public readonly static WaitForSeconds WAIT_TIME_TWO = new WaitForSeconds(2f);
    public readonly static WaitForSeconds WAIT_TIME_THREE = new WaitForSeconds(3f);
    public readonly static WaitForSeconds WAIT_TIME_FOUR = new WaitForSeconds(4f);
    public readonly static WaitForSeconds WAIT_TIME_FIVE = new WaitForSeconds(5f);
    public readonly static WaitForSeconds WAIT_TIME_EIGHT = new WaitForSeconds(8f);
}

public class GameTag
{
    public const string ENEMY = "Enemy";
    public const string PLAYER = "Player";
    public const string PLAYER_WEAPON = "PlayerWeapon";
    public const string FORTRESS = "Fortress";
    public const string CELL = "Cell";
}
