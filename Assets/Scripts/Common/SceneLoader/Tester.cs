using System;
using System.Text;
using AChen.Log;
using AChen.Prefabs;
using AChen.UI;
using Cysharp.Threading.Tasks;
using DialogueSystem;
using GamePlay.Gravity;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZZ.SceneLoader;

public class Tester : MonoBehaviour
{

    void Start()
    {
    }
    [Button]
    void ShowDia()=>DialogueRunner.Instance.ShowDialogue();
    [Button]
    void SelectDia(int index)=>DialogueRunner.Instance.SelectChoice(index);
    [Button]
    void Advance()=>DialogueRunner.Instance.Advance();
    

}
