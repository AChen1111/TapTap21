using UnityEngine;

namespace GamePlay.Wind
{
    /// <summary>
    /// 根据风场规则计算升举/悬浮力并 AddForce，也支持传统恒定风力。
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public class WindBody2D : MonoBehaviour
    {
        [SerializeField, Min(0f), Tooltip("受风倍率，0 表示不受风影响。")]
        private float _response = 1f;

        [SerializeField, Tooltip("自动对动态刚体施力；由外部移动代码接管时关闭，避免重复施力。")]
        private bool _autoApply = true;

        private Rigidbody2D _body;
        private WindField2D _hoverField;

        public float Response
        {
            get => Mathf.Max(0f, _response);
            set => _response = Mathf.Max(0f, value);
        }

        public bool AutoApply
        {
            get => _autoApply;
            set => _autoApply = value;
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void OnDisable()
        {
            _hoverField = null;
        }

        /// <summary>实时读取质心位置的风力，包含受风倍率；组件停用时返回零。</summary>
        public Vector2 GetForce()
        {
            if (!isActiveAndEnabled)
            {
                return Vector2.zero;
            }

            if (_body == null)
            {
                _body = GetComponent<Rigidbody2D>();
            }

            if (Response <= 0f)
            {
                _hoverField = null;
                return Vector2.zero;
            }
            Vector2 position = _body.worldCenterOfMass;
            _hoverField = WindField2D.SelectHoverField(position, _hoverField);
            Vector2 force = WindField2D.SampleConstantForce(position) * Response;
            if (_hoverField != null)
            {
                force += _hoverField.CalculateHoverForce(position, _body.linearVelocity,
                    _body.mass, Physics2D.gravity * _body.gravityScale, Time.fixedTime, Response);
            }
            return force;
        }

        private void FixedUpdate()
        {
            if (!_autoApply || !_body.simulated || _body.bodyType != RigidbodyType2D.Dynamic)
            {
                return;
            }

            // 升举模式根据质量换算所需力，固定风模式不乘质量。
            // Force 模式由物理引擎处理时间步长，此处不再乘 fixedDeltaTime。
            _body.AddForce(GetForce(), ForceMode2D.Force);
        }
    }
}
