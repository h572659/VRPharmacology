using UnityEngine;

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
}


