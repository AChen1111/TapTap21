using UnityEngine;
/// <summary>
/// 挂载这个脚本的GO,在场景切换时不会被销毁
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
