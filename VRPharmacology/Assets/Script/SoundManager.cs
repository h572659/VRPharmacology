using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour
{
   [Header("Audio Source")]
   [SerializeField] private AudioSource music;
   [SerializeField] private AudioSource SFX;
   [Header("Audio Clip")]
   public AudioClip menu;
   public AudioClip before;
   public AudioClip start;
   public AudioClip hoverSound;
   public AudioClip correctAnswer;
   public AudioClip wrongAnswer;
   
   [Header("Audio Mixer")]
   [SerializeField] private AudioMixer AM;
   [SerializeField] private Slider masterSlider;
   [SerializeField] private Slider musicSlider;
   [SerializeField] private Slider SFXSlider;
   [SerializeField] private GameObject meny;

    public void toggleMeny(){
        meny.SetActive(!meny.activeSelf);
    }
    public void PlaySFX(AudioClip Sound){
        SFX.clip = Sound;
        SFX.Play();
    }
    public void PlayMusic(AudioClip Music) {
        music.clip = Music;
        music.Play();
    }
    public void StopMusic(){
        music.Stop();
        music.clip = null;
    }

    public void muteSound(){
        music.mute = true;
        SFX.mute = true;
    }
    public void unmuteSound(){
        music.mute = false;
        SFX.mute = false;
    }
    public void setMusicVolume(){
        float volume = musicSlider.value;
        AM.SetFloat("MusicV", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void setMasterVolume(){
        float volume = masterSlider.value;
        AM.SetFloat("MasterV", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }
    public void setSFXVolume(){
        float volume = SFXSlider.value;
        AM.SetFloat("SFXV", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    private void Start() {
        if (PlayerPrefs.HasKey("MusicVolume")){
            LoadVolumeMusic();
        }
        if (PlayerPrefs.HasKey("SFXVolume")){
            LoadVolumeSFX();
        }
        if (PlayerPrefs.HasKey("MasterVolume")){
            LoadVolumeMaster();
        }
        setSFXVolume();
        setMasterVolume();
        setMusicVolume();
    }

    private void LoadVolumeMusic(){
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }
    private void LoadVolumeSFX(){
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
    private void LoadVolumeMaster(){
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
    }
}


