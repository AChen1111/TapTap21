
using AChen.Events;
using AChen.Log;
using AChen.UI;
using GamePlay.Core;
using GamePlay.Inventory;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public UISettings uI;
    void Start()
    {
        uI.CreateUIInstance();
        InventoryModel model=new(10);
        EventCenter.Dispatch(GameEvent.ModelBinded,model);
        model.PutItem(new Item_Tree(),2);
        model.PutItem(new Item_Water(),1);
    }
}
