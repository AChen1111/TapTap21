using System.Collections.Generic;
using AChen.Events;
using GamePlay.Player.Movement;
using UnityEngine;

/// <summary>自动过图或进入范围后按交互键过图，共用场景管理器的异步淡入淡出。</summary>
[RequireComponent(typeof(BoxCollider2D))]
public sealed class MapExit2D : MonoBehaviour
{
    public enum ActivationMode { Automatic, Interact }
    [SerializeField] private ActivationMode _activation;
    [SerializeField] private string _targetScene;
    [SerializeField] private string _targetSpawnId = "Default";
    [SerializeField, Tooltip("可选的 F 提示对象，进入交互范围才显示。")]
    private GameObject _interactionPrompt;
    private readonly HashSet<Collider2D> _contacts = new HashSet<Collider2D>();

    private void Reset() => GetComponent<BoxCollider2D>().isTrigger = true;
    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        EventCenter.AddListener(GameEvent.OnInteractKeyPressed, Interact);
        ShowPrompt(false);
    }
    private void OnDisable()
    {
        EventCenter.RemoveListener(GameEvent.OnInteractKeyPressed, Interact);
        _contacts.Clear();
        ShowPrompt(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponentInParent<PlayerMovement>() == null) return;
        _contacts.Add(other);
        ShowPrompt(_activation == ActivationMode.Interact);
        if (_activation == ActivationMode.Automatic) TryTravel();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (_activation == ActivationMode.Automatic && _contacts.Contains(other)) TryTravel();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        _contacts.Remove(other);
        if (_contacts.Count == 0) ShowPrompt(false);
    }
    private void Interact()
    {
        _contacts.RemoveWhere(c => c == null || !c.gameObject.activeInHierarchy);
        if (_activation == ActivationMode.Interact && _contacts.Count > 0) TryTravel();
    }
    public void TryTravel()
    {
        var manager = FindFirstObjectByType<GameSceneManager>();
        if (manager == null || !manager.CanUseExit) return;
        manager.LoadMap(_targetScene, _targetSpawnId);
    }
    private void ShowPrompt(bool visible)
    {
        if (_interactionPrompt != null && _interactionPrompt != gameObject) _interactionPrompt.SetActive(visible);
    }
}
