using GamePlay.Player.Anim;
using GamePlay.Player.Movement;
using UnityEngine;

namespace GamePlay.Player.Audio
{
    /// <summary>跑步时按跑步动画节奏播脚步，跳跃成功时播跳跃音。</summary>
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(100)]
    [RequireComponent(typeof(PlayerMovement))]
    public sealed class PlayerLocomotionAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _footsteps;
        [SerializeField] private AudioClip[] _jumps;
        [SerializeField, Range(0f, 1f)] private float _footstepVolume = 0.55f;
        [SerializeField, Range(0f, 1f)] private float _jumpVolume = 0.85f;

        private AudioSource _source;
        private PlayerMovement _movement;
        private PlayerAnimatorController _animatorController;
        private Animator _animator;
        private float _heardJumpTime = float.NegativeInfinity;
        private float _stepTimer = -1f;
        private bool _playingJump;
        private bool _paused;

        private void Awake()
        {
            _movement = GetComponent<PlayerMovement>();
            _animatorController = GetComponent<PlayerAnimatorController>();
            _animator = GetComponent<Animator>();
            _source = GetComponent<AudioSource>();
            if (_source == null) _source = gameObject.AddComponent<AudioSource>();
            _source.playOnAwake = false;
            _source.loop = false;
            _source.spatialBlend = 0f;
            _source.dopplerLevel = 0f;
        }

        private void OnEnable()
        {
            _heardJumpTime = _movement.LastJumpTime;
            _stepTimer = -1f;
            _playingJump = false;
            _paused = false;
        }

        private void OnDisable()
        {
            _stepTimer = -1f;
            _playingJump = false;
            _paused = false;
            if (_source != null) _source.Stop();
        }

        private void LateUpdate()
        {
            if (_source == null || _movement == null) return;
            if (!_movement.isActiveAndEnabled)
            {
                _heardJumpTime = _movement.LastJumpTime;
                _stepTimer = -1f;
                _playingJump = false;
                StopPlayback();
                return;
            }

            if (Time.timeScale <= 0f)
            {
                if (!_paused)
                {
                    _paused = true;
                    _source.Pause();
                }
                return;
            }

            if (_paused)
            {
                _paused = false;
                _source.UnPause();
            }

            float jumpTime = _movement.LastJumpTime;
            if (jumpTime != _heardJumpTime)
            {
                _heardJumpTime = jumpTime;
                _stepTimer = -1f;
                Play(Pick(_jumps), _jumpVolume, false);
            }

            if (_animatorController == null || !_animatorController.IsRunning || Time.time - jumpTime < 0.05f)
            {
                _stepTimer = -1f;
                if (!_playingJump) StopPlayback();
                return;
            }

            float speed = _animator != null ? Mathf.Max(_animator.speed, 0.15f) : 1f;
            if (_stepTimer < 0f) _stepTimer = 0.3f;
            _stepTimer += Time.deltaTime * speed;
            if (_stepTimer < 0.3f) return;
            _stepTimer -= 0.3f;
            Play(Pick(_footsteps), _footstepVolume, true);
        }

        private void Play(AudioClip clip, float volume, bool varyPitch)
        {
            if (clip == null)
            {
                _playingJump = false;
                StopPlayback();
                return;
            }

            _playingJump = !varyPitch;
            _source.spatialBlend = 0f;
            _source.pitch = varyPitch ? Random.Range(0.96f, 1.04f) : 1f;
            _source.volume = volume;
            _source.clip = clip;
            _source.Play();
        }

        private void StopPlayback()
        {
            bool paused = _paused;
            _paused = false;
            if (paused || _source.isPlaying) _source.Stop();
        }

        private static AudioClip Pick(AudioClip[] clips)
        {
            if (clips == null || clips.Length == 0) return null;
            int start = Random.Range(0, clips.Length);
            for (int i = 0; i < clips.Length; i++)
            {
                AudioClip clip = clips[(start + i) % clips.Length];
                if (clip != null) return clip;
            }

            return null;
        }
    }
}
