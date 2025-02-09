using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {get; private set;}
    private AudioSource soundSource;
    private AudioSource musicSource;
    private AudioSource menuMusicSource;

    private void Awake(){
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();
        menuMusicSource = transform.GetChild(1).GetComponent<AudioSource>();

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this) {
            Destroy(gameObject);
        }

        ChangeMusicVolume(0);
        ChangeSoundVolume(0);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0) {
            PlayMenuMusic(); //menu music
        }
        else {
            PlayMusic(); //game music
        }
    }

    public void PlaySound(AudioClip _sound){
        soundSource.PlayOneShot(_sound);
    }

    public void PlayMusic() {
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayMenuMusic() {
        menuMusicSource.loop = true;
        menuMusicSource.Play();
    }

    public void StopMusic() {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void StopMenuMusic() {
        if (menuMusicSource.isPlaying)
        {
            menuMusicSource.Stop();
        }
    }

    public void ChangeSoundVolume(float _change) {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }

    public void ChangeMusicVolume(float _change) {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source) {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        if(currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
