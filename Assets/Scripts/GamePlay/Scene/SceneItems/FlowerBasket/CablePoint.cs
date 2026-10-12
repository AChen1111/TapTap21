using GamePlay.Scene;
using Sirenix.OdinInspector;
using UnityEngine;
using AChen.Events;
using AChen.Log;
namespace GamePlay{
public class CablePoint : MonoBehaviour,ISceneItem,ICanInteract
{
    
    [SerializeField]private FlowerBasket _flowerBasket;
    [SerializeField]private CablePoint _other;
    [field:SerializeField]public bool IsStay{get;set;}

    void Awake()
    {
        
    }


    public void OnEnterInteract()
    {
        EventCenter.AddListener(GameEvent.OnInteractKeyPressed,CallFlowerBasket);
        ALog.Log("in!");
    }

    public void OnExitInteract()
    {
        EventCenter.RemoveListener(GameEvent.OnInteractKeyPressed,CallFlowerBasket);
        ALog.Log("out!");
    }



    void CallFlowerBasket()
    {
        if (IsStay)
        {
            EventCenter.Dispatch(GameEvent.CallFlowerBasket,_other.transform.position);
            IsStay=false;
            _other.IsStay=true;
        }
        else
        {
            EventCenter.Dispatch(GameEvent.CallFlowerBasket,transform.position);
            IsStay=true;
            _other.IsStay=false;
        }
        
    }
}
}