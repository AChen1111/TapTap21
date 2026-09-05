using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;
using ZZ.ZTween;
public class Test : MonoBehaviour
{
    public GameObject go;

    void Start()
    {
        go.GetComponent<RectTransform>().ZPopup();
    }
}
