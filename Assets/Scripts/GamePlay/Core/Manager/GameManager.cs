using AChen.Events;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        EventCenter.Dispatch(GameEvent.GameStarted);
    }
    [Button]
    void OnGameStart()
    {
        
    }
}
