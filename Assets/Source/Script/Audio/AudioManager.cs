using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip[] sfxClips;
    public AudioClip[] bgmClips;
    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        bgmSource.volume = musicVol;
        sfxSource.volume = sfxVol;
    }
    public void PlayBGM(string name)
    {
        AudioClip clip = FindClip(bgmClips, name);
        if (clip != null)
        {
            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("BGM not found: " + name);
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }
    public void PlaySFX(string name)
    {
        AudioClip clip = FindClip(sfxClips, name);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found: " + name);
        }
    }
    private AudioClip FindClip(AudioClip[] clips, string name)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == name)
                return clip;
        }
        return null;
    }
    public void SetMusicVolume(float value)
    {
        bgmSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }
    public float GetBGMVolume() => bgmSource.volume;
    public float GetSFXVolume() => sfxSource.volume;
}
