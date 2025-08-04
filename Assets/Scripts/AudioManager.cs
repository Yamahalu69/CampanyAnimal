using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField]
    private AudioSource bgmSource, seSource;

    [SerializeField]
    private AudioClip title, playing, gameover, gameclear, uiclick, warning;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TitleBGM()
    {
        bgmSource.clip = title;
        bgmSource.Play();
    }

    public void PlayingBGM()
    {
        bgmSource.clip = playing;
        bgmSource.Play();
    }

    public void GameoverBGM()
    {
        bgmSource.clip = gameover;
        bgmSource.Play();
    }

    public void GameclearBGM()
    {
        bgmSource.clip = gameclear;
        bgmSource.Play();
    }

    public void WarningSE()
    {
        seSource.PlayOneShot(warning);
    }

    public void UIClick()
    {
        seSource.PlayOneShot(uiclick);
    }

    void Start()
    {
        TitleBGM();
    }
}
