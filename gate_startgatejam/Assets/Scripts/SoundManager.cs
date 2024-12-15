using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource sfxSource;
    public AudioSource bgmSource;
    public AudioSource dialogueSource;

    [Header("Audio Clips")]
    public AudioClip doorOpenClip;
    public AudioClip doorCloseClip;
    public AudioClip doorDragClip;
    public AudioClip phonePickClip;
    public AudioClip phoneHangClip;
    public AudioClip footstepClip;
    public AudioClip unlockClip;
    public AudioClip backgroundLoopClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM()
    {
        if (bgmSource != null && backgroundLoopClip != null)
        {
            bgmSource.clip = backgroundLoopClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlayDialogue(AudioClip clip)
    {
        if (clip != null)
        {
            dialogueSource.clip = clip;
            dialogueSource.Play();
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void StopDialogue()
    {
        dialogueSource.Stop();
    }
}
