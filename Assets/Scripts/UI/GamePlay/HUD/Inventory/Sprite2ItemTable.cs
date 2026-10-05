using System;
using System.Collections.Generic;
using AChen.Log;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprite2ItemTable", menuName = "Scriptable Objects/Sprite2ItemTable")]
public class Sprite2ItemTable : ScriptableObject
{
    private Dictionary<string,Sprite> _table=new();

    [ShowInInspector]private List<string> _typeNames;
    [ShowInInspector]private List<Sprite> _sprites;  
    public Sprite2ItemTable()
    {
        
    }
    public void UpdateDictInfo()
    {
        _table=new();
        if (_typeNames.Count != _sprites.Count)
        {
            ALog.LogError("键值对数量不匹配!");
            return;
        }
        for(int i = 0; i < _typeNames.Count; ++i)
        {
            _table.Add(_typeNames[i],_sprites[i]);
        }
    }
    public Sprite GetSprite(Type t)
    {
        var typeinfo=t.ToString();
        if (_table.ContainsKey(typeinfo))
        {
            return _table[typeinfo];
        }
        else
        {
            ALog.LogError("Type: "+typeinfo+" 不在table里");
            return null;
        }
    }
}
