using System.Collections;
using UnityEngine;

public class AttackUIControl : MonoBehaviour
{
    public bool enableHoldAttackBasic;
    private bool pressed;
    public void StartBasicAttack()
    {
        pressed = true;
    }

    public void EndBasicAttack()
    {
        pressed = false;
    }

    public void BasicAttackClick()
    {
        EventManager.CallEvent(GameEvent.ATTACK_CHARACTER);
    }

    public void Log()
    {
        LogSystem.LogError("Enter");

    }

    private void Update()
    {
        if(pressed && Time.frameCount % 10 == 0)
            EventManager.CallEvent(GameEvent.ATTACK_CHARACTER);
    }
}
