using dyh;
using Sirenix.OdinInspector;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [Button]
    public void PlayMusic()
    {
        AudioManager.Instance.PlayMusic(AudioIds.Music);
    }
    [Button]
    public void StopMusic()
    {
        AudioManager.Instance.StopMusic();
    }
    //public void MuiscQuitLoop() => AudioManager.Instance.StopLoop(AudioIds.Music);
}
