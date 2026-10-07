using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Wind
{
    /// <summary>起点式窄矩形风场，支持快速升举、末端悬浮或传统恒定风力。</summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(BoxCollider2D))]
    public class WindField2D : MonoBehaviour
    {
        private static readonly HashSet<WindField2D> _activeFields = new HashSet<WindField2D>();
        [SerializeField] private Vector2 _direction = Vector2.up;
        [SerializeField, Min(0f), Tooltip("风力，不是加速度。")]
        private float _strength = 5f;
        [SerializeField, Min(0.01f)] private float _length = 6f;
        [SerializeField, Min(0.01f)] private float _width = 1f;
        [SerializeField, Tooltip("开启快速升举和末端悬浮；关闭则使用固定风力。")]
        private bool _hoverAtEnd = true;
        [SerializeField, Min(0.01f)] private float _riseSpeed = 6f;
        [SerializeField, Min(0.01f)] private float _speedResponseTime = 0.15f;
        [SerializeField, Min(0.01f)] private float _slowdownDistance = 1.5f;
        [SerializeField, Min(0f)] private float _hoverInset = 0.4f;
        [SerializeField, Min(0f)] private float _hoverAmplitude = 0.1f;
        [SerializeField, Min(0f)] private float _hoverFrequency = 0.6f;
        [SerializeField, Min(0f)] private float _positionGain = 36f;
        [SerializeField, Min(0.01f)] private float _damping = 12f;
        [SerializeField, Min(0.01f)] private float _maxAcceleration = 30f;
        [SerializeField, Min(0f), Tooltip("已进入风场的物体越过末端后仍受控的距离。")]
        private float _endBuffer = 0.5f;
        private BoxCollider2D _area;

        public Vector2 Origin => transform.position;
        public Vector2 Direction => _direction.normalized;
        public float Strength => Mathf.Max(0f, _strength);
        public float Length => Mathf.Max(0.01f, _length);
        public float Width => Mathf.Max(0.01f, _width);
        public bool HoverAtEnd
        {
            get => _hoverAtEnd;
            set => _hoverAtEnd = value;
        }
        public float HoverDistance => Mathf.Max(0f, Length - Mathf.Clamp(_hoverInset, 0f, Length));

        private bool IsProvidingWind => isActiveAndEnabled && _area != null &&
            _area.enabled && Strength > 0f && Direction != Vector2.zero;

        private void Awake()
        {
            UpdateArea();
        }

        private void Reset()
        {
            UpdateArea();
        }

        private void OnValidate()
        {
            UpdateArea();
        }

        private void OnEnable()
        {
            UpdateArea();
            _activeFields.Add(this);
        }

        private void OnDisable()
        {
            _activeFields.Remove(this);
        }

        private void OnDestroy()
        {
            _activeFields.Remove(this);
        }

        private void UpdateArea()
        {
            _area = GetComponent<BoxCollider2D>();
            if (_area == null)
            {
                return;
            }
            _area.isTrigger = true;
            _area.size = new Vector2(Length, Width);
            _area.offset = new Vector2(Length * 0.5f, 0f);
            if (Direction != Vector2.zero)
            {
                transform.rotation = Quaternion.Euler(0f, 0f,
                    Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg);
            }
        }

        /// <summary>由放置逻辑传入世界起点、选定方向；零方向视为无风。</summary>
        public void Place(Vector2 origin, Vector2 direction)
        {
            transform.position = new Vector3(origin.x, origin.y, transform.position.z);
            SetWind(direction, Strength);
        }

        public void SetWind(Vector2 direction, float strength)
        {
            _direction = direction;
            _strength = Mathf.Max(0f, strength);
            UpdateArea();
        }

        public void SetSize(float length, float width)
        {
            _length = Mathf.Max(0.01f, length);
            _width = Mathf.Max(0.01f, width);
            UpdateArea();
        }

        /// <summary>起点位于短边中心，背后不受风。按世界尺寸判断。</summary>
        public bool Contains(Vector2 worldPosition)
        {
            if (Direction == Vector2.zero)
            {
                return false;
            }
            Vector2 delta = worldPosition - Origin;
            Vector2 side = new Vector2(-Direction.y, Direction.x);
            float forward = Vector2.Dot(delta, Direction);
            return forward >= 0f && forward <= Length &&
                Mathf.Abs(Vector2.Dot(delta, side)) <= Width * 0.5f;
        }

        public Vector2 GetForce(Vector2 worldPosition)
        {
            if (!isActiveAndEnabled || _area == null || !_area.enabled || !Contains(worldPosition))
            {
                return Vector2.zero;
            }
            return Direction * Strength;
        }

        public static Vector2 SampleTotalForce(Vector2 worldPosition)
        {
            Vector2 force = Vector2.zero;
            foreach (WindField2D field in _activeFields)
            {
                if (field != null)
                {
                    force += field.GetForce(worldPosition);
                }
            }
            return force;
        }

        /// <summary>悬浮模式不叠加多个位置控制器；保留当前风场，否则选强度最大的覆盖风场。</summary>
        public static WindField2D SelectHoverField(Vector2 worldPosition, WindField2D current)
        {
            if (current != null && current.HoverAtEnd && current.IsProvidingWind &&
                current.ContainsControlPoint(worldPosition))
            {
                return current;
            }
            WindField2D selected = null;
            foreach (WindField2D field in _activeFields)
            {
                if (field == null || !field.HoverAtEnd || !field.IsProvidingWind ||
                    !field.Contains(worldPosition))
                {
                    continue;
                }
                if (selected == null || field.Strength > selected.Strength ||
                    (field.Strength == selected.Strength && field.GetInstanceID() < selected.GetInstanceID()))
                {
                    selected = field;
                }
            }
            return selected;
        }

        public bool ContainsControlPoint(Vector2 worldPosition)
        {
            Vector2 delta = worldPosition - Origin;
            float distance = Vector2.Dot(delta, Direction);
            Vector2 side = new Vector2(-Direction.y, Direction.x);
            return Direction != Vector2.zero && distance >= 0f &&
                distance <= Length + Mathf.Max(0f, _endBuffer) &&
                Mathf.Abs(Vector2.Dot(delta, side)) <= Width * 0.5f;
        }

        public static Vector2 SampleConstantForce(Vector2 worldPosition)
        {
            Vector2 force = Vector2.zero;
            foreach (WindField2D field in _activeFields)
            {
                if (field != null && !field.HoverAtEnd)
                {
                    force += field.GetForce(worldPosition);
                }
            }
            return force;
        }

        /// <summary>根据位置和速度计算控制力；只控制沿风向的运动，补偿该轴重力。</summary>
        public Vector2 CalculateHoverForce(Vector2 position, Vector2 velocity, float mass,
            Vector2 gravityAcceleration, float time, float response = 1f)
        {
            if (!HoverAtEnd || !IsProvidingWind || response <= 0f || mass <= 0f ||
                !ContainsControlPoint(position))
            {
                return Vector2.zero;
            }
            float distance = Vector2.Dot(position - Origin, Direction);
            float speed = Vector2.Dot(velocity, Direction);
            float center = HoverDistance;
            // 短风场也保留摆动空间，目标始终位于末端内侧。
            float amplitude = Mathf.Min(Mathf.Max(0f, _hoverAmplitude),
                Mathf.Min(center, Length - center) * 0.8f);
            float omega = Mathf.Max(0f, _hoverFrequency) * Mathf.PI * 2f;
            float phase = time * omega;
            float target = center + Mathf.Sin(phase) * amplitude;
            float targetSpeed = Mathf.Cos(phase) * amplitude * omega;
            float targetAcceleration = -Mathf.Sin(phase) * amplitude * omega * omega;
            float cruiseAcceleration = (Mathf.Max(0f, _riseSpeed) - speed) /
                Mathf.Max(0.01f, _speedResponseTime);
            float hoverAcceleration = Mathf.Max(0f, _positionGain) * (target - distance) +
                Mathf.Max(0.01f, _damping) * (targetSpeed - speed) + targetAcceleration;
            float slowdown = Mathf.Min(Mathf.Max(0.01f, _slowdownDistance), Mathf.Max(0.01f, center));
            float blend = Mathf.SmoothStep(0f, 1f,
                Mathf.Clamp01((distance - (center - slowdown)) / slowdown));
            float limit = Mathf.Max(0.01f, _maxAcceleration) * (Strength / 5f) * response;
            float acceleration = Mathf.Clamp(
                Mathf.Lerp(cruiseAcceleration, hoverAcceleration, blend), -limit, limit);
            float gravityAlongWind = Vector2.Dot(gravityAcceleration, Direction);
            return Direction * mass * (acceleration - gravityAlongWind);
        }

        private void OnDrawGizmosSelected()
        {
            Color previous = Gizmos.color;
            Gizmos.color = Color.cyan;
            Vector3 origin = transform.position;
            Vector3 forward = Direction;
            Vector3 side = new Vector3(-Direction.y, Direction.x, 0f) * Width * 0.5f;
            Vector3 end = origin + forward * Length;
            Gizmos.DrawLine(origin - side, origin + side);
            Gizmos.DrawLine(origin - side, end - side);
            Gizmos.DrawLine(origin + side, end + side);
            Gizmos.DrawLine(end - side, end + side);
            Gizmos.DrawLine(origin, end);
            Gizmos.DrawLine(end, end - forward * 0.3f + side * 0.3f);
            Gizmos.DrawLine(end, end - forward * 0.3f - side * 0.3f);
            Gizmos.color = previous;
        }
    }
}
