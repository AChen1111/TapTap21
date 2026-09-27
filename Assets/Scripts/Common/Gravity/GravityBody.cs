using UnityEngine;
using AChen.Log;
using AChen.Events;
using System.Reflection.Metadata;

namespace GamePlay.Gravity
{
    public class GravityBody : MonoBehaviour
    {
        private Rigidbody2D _rb2d;

        private float _originGravityScale;

        [Tooltip("重力翻转时将物体沿X轴翻转")]
        public bool FlipX = false;
        [Tooltip("重力翻转时将物体沿Y轴翻转")]
        public bool FlipY = true;

        private void Awake()
        {
            if (TryGetComponent<Rigidbody2D>(out _rb2d) == false)
            {
                ALog.LogError($"{name} 使用了 GravityBody 但未挂载 \"Rigidbody2D\" 组件!");
                enabled = false;
                return;
            }
            _originGravityScale = _rb2d.gravityScale;
        }

        private void Start()
        {
            int dir = (int)GravityService.Instance.Direction;
            float stg = GravityService.Instance.Strength;
            ApplyGravity(dir * stg);
        }

        private void OnEnable()
        {
            EventCenter.AddListener<float, EGravityDirection>(GameEvent.GravityChanged, OnGravityChanged);
            EventCenter.AddListener<EGravityDirection>(GameEvent.GravityFlipped, OnGravityFlipped);
        }

        private void OnDisable()
        {
            EventCenter.RemoveListener<float, EGravityDirection>(GameEvent.GravityChanged, OnGravityChanged);
            EventCenter.RemoveListener<EGravityDirection>(GameEvent.GravityFlipped, OnGravityFlipped);
        }

        private void ApplyGravity(float gvt)
        {
            _rb2d.gravityScale = gvt * _originGravityScale;
        }

        private void OnGravityChanged(float gvt, EGravityDirection dir)
        {
            ApplyGravity(gvt);
        }

        private void OnGravityFlipped(EGravityDirection dir)
        {
            Vector3 currentScale = transform.localScale;
            Vector3 targetScale = currentScale;
            if (FlipY)
            {
                targetScale = new Vector3(targetScale.x, targetScale.y * (-(int)dir) * (int)dir, targetScale.z);
            }
            if (FlipX)
            {
                targetScale = new Vector3(targetScale.x * (-(int)dir) * (int)dir, targetScale.y, targetScale.z);
            }

            transform.localScale = targetScale;

        }
    }
}