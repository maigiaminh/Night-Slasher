using System.Collections.Generic;
using UnityEngine;

public abstract class FSMSystem : MonoBehaviour
{
    private EnemyState currentState;

    public void Update()
    {
        currentState?.UpdateState(Time.deltaTime);
    }

    public void ChangeState(EnemyState newState)
    {
        currentState?.ExitState();
        currentState = newState;
        currentState?.EnterState();
    }
}
