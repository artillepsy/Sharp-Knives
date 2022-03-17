using Core;
using LevelSettings;
using SaveSystem;
using Scriptable;
using UnityEngine;

namespace Management
{
    public class SoundManager : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private AudioClip knifeDropAudio;
        [SerializeField] private AudioClip appleHitAudio;
        [Header("Runtime changeable")]
        [SerializeField] private AudioClip logHitAudio;
        [SerializeField] private AudioClip logDestroyAudio;
        private AudioSource _audioSource;
        private SaveManager _saveManager;
        private bool _vibration;

        public void ChangeAudioVolume(float volume)
        {
            _audioSource.volume = volume;
            _saveManager.Sound.SetVolumeSettings(_audioSource.volume, _vibration);
        }

        public void ChangeVibration(bool vibrate)
        {
            _vibration = vibrate;
            _saveManager.Sound.SetVolumeSettings(_audioSource.volume, _vibration);
        }
        public void OnLevelLoad(Level level)
        {
            if (!level.Log.Custom.IsBoss) return;
            if(level.Log.Custom.Boss.HitAudio) logHitAudio = level.Log.Custom.Boss.HitAudio;
            if(level.Log.Custom.Boss.DestroyAudio) logDestroyAudio = level.Log.Custom.Boss.DestroyAudio;
        }
        private void OnEnable()
        {
            Vibration.Init();
            _audioSource = GetComponent<AudioSource>();
            Events.OnKnifeDrop.AddListener(()=>
            {
                if(_vibration) Vibration.Vibrate(400);
                PlayClip(knifeDropAudio);
            });
            Events.OnAppleHit.AddListener(()=>PlayClip(appleHitAudio));
            Events.OnKnifeHit.AddListener(()=>
            {
                if(_vibration) Vibration.VibratePop();
                PlayClip(logHitAudio);
            });
            Events.OnWinGame.AddListener(()=>
            {
                if(_vibration) Vibration.Vibrate(400);
                PlayClip(logDestroyAudio);
            });
        }

        private void Start()
        {
            _saveManager = FindObjectOfType<SaveManager>();
            _audioSource.volume = _saveManager.Sound.Volume;
            _vibration = _saveManager.Sound.Vibration;
        }

        private void PlayClip(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }
    }
}