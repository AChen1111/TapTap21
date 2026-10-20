using AChen.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.Audio
{
    /// <summary>菜单与关卡的循环背景音乐，以及从物品栏拖出物品的音效。</summary>
    [DisallowMultipleComponent]
    public sealed class GameAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] private AudioClip _gameMusic;
        [SerializeField] private AudioClip _pullItem;
        [SerializeField, Range(0f, 1f)] private float _musicVolume = 0.4f;
        [SerializeField, Range(0f, 1f)] private float _pullVolume = 0.85f;

        private AudioSource _music;
        private AudioSource _effect;

        private void Awake()
        {
            _music = CreateSource(true);
            _effect = CreateSource(false);
        }

        private void OnEnable()
        {
            EventCenter.AddListener(GameEvent.OnGameSceneLoaded, PlayGame);
            EventCenter.AddListener(GameEvent.SlotBeginDragged, PlayPull);
        }

        private void OnDisable()
        {
            EventCenter.RemoveListener(GameEvent.OnGameSceneLoaded, PlayGame);
            EventCenter.RemoveListener(GameEvent.SlotBeginDragged, PlayPull);
        }

        private void Start()
        {
            string scene = SceneManager.GetActiveScene().name;
            PlayMusic(scene == "Menu" || scene == "PersistentScene" ? _menuMusic : _gameMusic);
        }

        private void PlayGame() => PlayMusic(_gameMusic);

        private void PlayMusic(AudioClip clip)
        {
            if (clip == null || _music == null) return;
            if (clip.loadState == AudioDataLoadState.Unloaded) clip.LoadAudioData();
            if (_music.clip == clip && _music.isPlaying) return;
            _music.spatialBlend = 0f;
            _music.volume = _musicVolume;
            _music.clip = clip;
            _music.Play();
        }

        private void PlayPull(GameObject dragObject)
        {
            if (dragObject == null || _pullItem == null || _effect == null) return;
            if (_pullItem.loadState == AudioDataLoadState.Unloaded) _pullItem.LoadAudioData();
            _effect.spatialBlend = 0f;
            _effect.PlayOneShot(_pullItem, _pullVolume);
        }

        private AudioSource CreateSource(bool loop)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.loop = loop;
            source.spatialBlend = 0f;
            source.dopplerLevel = 0f;
            return source;
        }
    }
}
