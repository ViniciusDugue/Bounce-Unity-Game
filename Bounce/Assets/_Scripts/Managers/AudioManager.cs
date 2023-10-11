using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    // private void OnEnable() 
    // {
        
    // }
    public void StartMainMenuAudio()
    {
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopMainMenuAudio()
    {
        audioSource.Stop();
    }
}

// public class SoundController : MonoBehaviour
// {
//     public static bool SoundOn { get; private set; }

//     static SoundController _i;

//     [RuntimeInitializeOnLoadMethod()]
//     static void Prepare()
//     {
//         SoundOn = PlayerPrefs.GetInt(C.PPSoundOff, 0) == 0;

//         DontDestroyOnLoad(Instantiate(Resources.Load("SoundController")));
//     }

//     public static void Toggle()
//     {
//         SoundOn = !SoundOn;
//         PlayerPrefs.SetInt(C.PPSoundOff, SoundOn ? 0 : 1);

//         _i.SetOnOff();
//     }
    
//     static void Play(AudioSource audio)
//     {
//         audio.Stop();
//         audio.Play();
//     }

//     #pragma warning disable CS0649
//     public static void Click() => Play(_i._click);
//     [SerializeField] AudioSource _click;

//     public static void Close() => Play(_i._close);
//     [SerializeField] AudioSource _close;


//     public static void ProjectileBurn() => Play(_i._projectileBurn);
//     [SerializeField] AudioSource _projectileBurn;

//     public static void ProjectileSlow() => Play(_i._projectileSlow);
//     [SerializeField] AudioSource _projectileSlow;

//     public static void ProjectileSpeed() => Play(_i._projectileSpeed);
//     [SerializeField] AudioSource _projectileSpeed;

//     public static void ProjectileSpread() => Play(_i._projectileSpread);
//     [SerializeField] AudioSource _projectileSpread;

//     public static void Win() => Play(_i._win);
//     [SerializeField] AudioSource _win;

//     [SerializeField] AudioMixer _mixer;
//     #pragma warning restore CS0649

//     void OnEnable() => _i = this;

//     void Start() => SetOnOff();

//     void SetOnOff() =>
//         _mixer.SetFloat("volume", SoundOn ? 0 : -100f);
// }
