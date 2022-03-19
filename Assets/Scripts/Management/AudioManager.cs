using System.Collections.Generic;
using Core;
using LevelSettings;
using SaveSystem;
using Scriptable;
using UnityEngine;

namespace Management
{
    public class AudioManager : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private List<AudioClip> knifeDropClips;
        [SerializeField] private List<AudioClip> OnThrowClips;
        [SerializeField] private List<AudioClip> appleHitClips;
        [Header("Runtime changeable")]
        [SerializeField] private List<AudioClip> logHitClips;
        [SerializeField] private List<AudioClip> logDestroyClips;
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
            if (level.Log.Custom.Boss.HitClips.Count != 0)
            {
                logHitClips = level.Log.Custom.Boss.HitClips;
            }
            if (level.Log.Custom.Boss.DestroyClips.Count != 0)
            {
                logDestroyClips = level.Log.Custom.Boss.DestroyClips;
            }
        }
        private void OnEnable()
        {
            Vibration.Init();
            _audioSource = GetComponent<AudioSource>();
            Events.OnKnifeDrop.AddListener(()=>
            {
                if(_vibration) Vibration.Vibrate(400);
                PlayClip(knifeDropClips);
            });
            Events.OnAppleHit.AddListener(()=>PlayClip(appleHitClips));
            Events.OnKnifeHit.AddListener(()=>
            {
                if(_vibration) Vibration.VibratePop();
                PlayClip(logHitClips);
            });
            Events.OnWinGame.AddListener(()=>
            {
                if(_vibration) Vibration.Vibrate(400);
                PlayClip(logDestroyClips);
            });
            Events.OnThrow.AddListener(()=> PlayClip(OnThrowClips));
        }

        private void Start()
        {
            _saveManager = FindObjectOfType<SaveManager>();
            _audioSource.volume = _saveManager.Sound.Volume;
            _vibration = _saveManager.Sound.Vibration;
        }

        private void PlayClip(List<AudioClip> clips)
        {
            _audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
        }
    }
}