using System;
using UnityEngine;
namespace GamePlay.Player.Input
{
    [Flags]
    public enum EInputState
    {
        None=0,
        Move=1<<0,
        Jump=1<<1,
        ALL=Move|Jump,
    }
}
