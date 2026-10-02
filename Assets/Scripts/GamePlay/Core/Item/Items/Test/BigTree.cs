using AChen.Log;
using GamePlay.Core;
using UnityEngine;

public class BigTree : Item
{
    public override int State { get; set ; }

    public BigTree()
    {
        State=1;
    }
    public override Item CopyItem()
    {
        var ret=new BigTree();
        ret.State=this.State;
        return ret;       
    }

    public void SayCiallo()
    {
        ALog.Log("Ciallo!!");
    }
}
