using UnityEngine;
using FXSystem;
using Effects;

public class SpriteTest1 : MonoBehaviour
{
    public SpriteEffectProfile hitFlashProfile;
    public SpriteEffectProfile dissolveProfile;

    private void Awake()
    {

    }

    private void Start()
    {
        GetComponent<SpriteEffectPlayer>().Play(hitFlashProfile);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<SpriteEffectPlayer>().Play(dissolveProfile, () => { Debug.Log("Done"); });
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FX.PlayFollow("FX1", transform);
    }
}
