
using AChen.Log;
using GamePlay.Core;
using Mono.Cecil.Cil;
using UnityEngine;

public class Tester : MonoBehaviour
{
    Tree t=new();
    Water w=new();
    void Start()
    {
        var bt=MergeUtil.Merge<BigTree>(t,w,1,()=>ALog.LogError("FFF"));
        bt.SayCiallo();
    }
}
