using UnityEngine;

namespace GamePlay.Wind
{
    /// <summary>从风场起点发射 Stylised Wind 风线，同步风向、宽度、长度和启停。</summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(WindField2D), typeof(ParticleSystem))]
    public class WindVisual2D : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] private float _lineSpeed = 3f;
        [SerializeField, Min(0f)] private float _rateOverTime = 15f;
        private WindField2D _field;
        private ParticleSystem _particles;
        private Vector2 _lastDirection;
        private Vector2 _lastOrigin;
        private Vector4 _lastSettings;
        private bool _configured;

        private void Awake()
        {
            _field = GetComponent<WindField2D>();
            _particles = GetComponent<ParticleSystem>();
        }

        private void OnEnable()
        {
            _configured = false;
        }

        private void OnDisable()
        {
            if (_particles != null)
            {
                _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            }
        }

        private void LateUpdate()
        {
            bool shouldPlay = _field.isActiveAndEnabled && _field.Strength > 0f &&
                _field.Direction != Vector2.zero && GetComponent<BoxCollider2D>().enabled;
            if (!shouldPlay)
            {
                if (_particles.isPlaying || _particles.particleCount > 0)
                {
                    _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
                _configured = false;
                return;
            }

            Vector4 settings = new Vector4(_field.Length, _field.Width,
                Mathf.Max(0.01f, _lineSpeed), Mathf.Max(0f, _rateOverTime));
            if (!_configured || _lastDirection != _field.Direction ||
                _lastOrigin != _field.Origin || _lastSettings != settings)
            {
                Configure(settings);
                _lastDirection = _field.Direction;
                _lastOrigin = _field.Origin;
                _lastSettings = settings;
                _configured = true;
            }
            if (!_particles.isPlaying)
            {
                _particles.Play();
            }
        }

        private void Configure(Vector4 settings)
        {
            _particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            var main = _particles.main;
            main.loop = true;
            main.playOnAwake = false;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.startSpeed = 0f;
            main.startLifetime = settings.x / settings.z;
            var shape = _particles.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Box;
            shape.position = Vector3.zero;
            shape.rotation = Vector3.zero;
            shape.scale = new Vector3(0.01f, settings.y, 0.01f);
            var emission = _particles.emission;
            emission.enabled = true;
            emission.rateOverTime = settings.w;
            var velocity = _particles.velocityOverLifetime;
            velocity.enabled = true;
            velocity.space = ParticleSystemSimulationSpace.World;
            velocity.x = _field.Direction.x * settings.z;
            velocity.y = _field.Direction.y * settings.z;
            velocity.z = 0f;
            var noise = _particles.noise;
            noise.enabled = false;
            var trails = _particles.trails;
            trails.enabled = true;
            trails.dieWithParticles = true;
        }
    }
}
