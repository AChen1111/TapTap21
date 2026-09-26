using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[DisallowMultipleComponent]
[ExecuteAlways]
public abstract class AdapterBase : MonoBehaviour
{
    // Initialize imported layouts even when polling is disabled.
    protected virtual void OnEnable() => Adapt();

    public abstract void Adapt();
}
