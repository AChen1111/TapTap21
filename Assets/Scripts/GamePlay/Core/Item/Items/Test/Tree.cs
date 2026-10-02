using GamePlay.Core;
using UnityEngine;
using UnityEngine.UIElements;

public class Tree : Item
{
    public override int State {get;set;}
    public Tree()
    {
        State=1;
    }
    public override Item CopyItem()
    {
        var ret=new Tree();
        ret.State=this.State;
        return ret;       
    }
}
