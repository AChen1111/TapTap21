using UnityEngine;

/// <summary>地图入口和重生位置，相同地图可配置不同 Id 的入口。</summary>
public sealed class MapSpawnPoint : MonoBehaviour
{
    [SerializeField] private string _spawnId = "Default";
    public string SpawnId => _spawnId;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
