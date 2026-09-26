using UnityEngine;

/// <summary>Optional example: attach to a GameObject to print the imported weapon table.</summary>
public class readBytesTest : MonoBehaviour
{
    private void Start()
    {
        foreach (var row in weapon.LoadBytes())
            Debug.Log($"{row.id}, {row.name}, {row.prefabName}, descriptions=[{string.Join(", ", row.desc)}], nums=[{string.Join(", ", row.nums)}]");
    }
}
