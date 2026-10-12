using GamePlay.Player.Health;
using UnityEngine;

namespace GamePlay.Environment.Hazard
{
    /// <summary>正常荆棘的接触伤害。玩家生命值由 PlayerHealth 统一管理。</summary>
    [DisallowMultipleComponent]
    public sealed class ThornHazard2D : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _damage = 1;
        [SerializeField, Min(0.05f)] private float _damageInterval = 0.8f;

        private float _nextDamageTime;

        private void OnCollisionStay2D(Collision2D collision)
        {
            TryDamage(collision.collider);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TryDamage(other);
        }

        private void TryDamage(Collider2D other)
        {
            if (other == null || Time.time < _nextDamageTime) return;
            PlayerHealth health = other.GetComponentInParent<PlayerHealth>();
            if (health == null || health.IsDead) return;

            health.TakeDamage(_damage);
            _nextDamageTime = Time.time + _damageInterval;
        }
    }
}
