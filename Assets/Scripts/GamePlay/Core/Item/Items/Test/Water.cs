using GamePlay.Core;
using UnityEngine;

public class Water : Item
{
    public override int State {get; set ; }

    public Water()
    {
        State=1;
    }

    public override Item CopyItem()
    {
        var ret=new Water();
        ret.State=this.State;
        return ret;       
    }
}
