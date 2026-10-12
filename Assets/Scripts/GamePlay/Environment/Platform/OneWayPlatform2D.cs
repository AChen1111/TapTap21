using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Environment.Platform
{
    /// <summary>
    /// 可从下方穿过、从上方站立的 2D 平台。
    /// 挂在平台根节点，根节点或子节点上的非 Trigger Collider 会自动接入 Effector。
    /// </summary>
    [RequireComponent(typeof(PlatformEffector2D))]
    public sealed class OneWayPlatform2D : MonoBehaviour
    {
        [SerializeField, Min(0.05f)] private float _dropThroughTime = 0.25f;
        [SerializeField] private bool _useOneWay = true;
        [SerializeField] private bool _useOneWayGrouping = true;
        [SerializeField] private bool _useSideFriction;
        [SerializeField] private bool _useSideBounce;

        private PlatformEffector2D _effector;
        private readonly HashSet<Collider2D> _ignoredPlayerColliders = new HashSet<Collider2D>();

        public float DropThroughTime => _dropThroughTime;

        private void Awake() => Configure();
        private void Reset() => Configure();

        private void Configure()
        {
            _effector = GetComponent<PlatformEffector2D>();
            _effector.useOneWay = _useOneWay;
            _effector.useOneWayGrouping = _useOneWayGrouping;
            _effector.useSideFriction = _useSideFriction;
            _effector.useSideBounce = _useSideBounce;

            foreach (Collider2D collider in GetComponentsInChildren<Collider2D>(true))
            {
                if (!collider.isTrigger)
                {
                    collider.usedByEffector = true;
                }
            }
        }

        /// <summary>让指定玩家临时穿过当前平台，时间到后自动恢复碰撞。</summary>
        public void DropThrough(GameObject player)
        {
            if (player == null) return;
            StartCoroutine(DropThroughRoutine(player));
        }

        private IEnumerator DropThroughRoutine(GameObject player)
        {
            Collider2D[] platformColliders = GetComponentsInChildren<Collider2D>(true);
            Collider2D[] playerColliders = player.GetComponentsInChildren<Collider2D>(true);
            _ignoredPlayerColliders.Clear();

            foreach (Collider2D playerCollider in playerColliders)
            {
                if (playerCollider == null || playerCollider.isTrigger) continue;
                foreach (Collider2D platformCollider in platformColliders)
                {
                    if (platformCollider == null || platformCollider.isTrigger) continue;
                    Physics2D.IgnoreCollision(playerCollider, platformCollider, true);
                    _ignoredPlayerColliders.Add(playerCollider);
                }
            }

            yield return new WaitForSeconds(_dropThroughTime);

            foreach (Collider2D playerCollider in _ignoredPlayerColliders)
            {
                if (playerCollider == null) continue;
                foreach (Collider2D platformCollider in platformColliders)
                {
                    if (platformCollider != null) Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
                }
            }
            _ignoredPlayerColliders.Clear();
        }
    }
}
