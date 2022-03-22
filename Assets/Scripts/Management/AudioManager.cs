using System.Collections.Generic;
using Core;
using SaveSystem;
using Scriptable;
using UnityEngine;

namespace Management
{
    /// <summary>
    /// Класс, отвечающий за звуковое сопровождение в игре
    /// </summary>
    public class AudioManager : MonoBehaviour, IOnLevelLoad
    {
        [SerializeField] private AudioClip buttonClick;
        [SerializeField] private AudioClip bossFightClip;
        [SerializeField] private AudioClip startLevelClip;
        [SerializeField] private AudioClip unlockKnifeClip;
        [SerializeField] private AudioClip equipKnifeClip;
        
        [SerializeField] private List<AudioClip> knifeDropClips;
        [SerializeField] private List<AudioClip> onThrowClips;
        [SerializeField] private List<AudioClip> appleHitClips;

        [Header("Runtime changeable")]
        [SerializeField] private List<AudioClip> logHitClips;
        [SerializeField] private List<AudioClip> logDestroyClips;
        private AudioSource _audioSource;
        private bool _vibration;
        private List<AudioClip> _logHitClips;
        private List<AudioClip> _logDestroyClips;

        /// <summary>
        /// Замена обычных звуков на звуки для босса (втыкание ножа в бревно и звук разбиения)
        /// </summary>
        /// <param name="level"></param>
        public void OnLevelLoad(Level level)
        {
            if (level.Log.Settings.IsBoss) PlayClip(bossFightClip);
            else PlayClip(startLevelClip);
            if (level.Log.Settings.Boss.HitClips.Count != 0)
            {
                _logHitClips = level.Log.Settings.Boss.HitClips;
            }
            else
            {
                _logHitClips = logHitClips;
            }
            if (level.Log.Settings.Boss.DestroyClips.Count != 0)
            {
                _logDestroyClips = level.Log.Settings.Boss.DestroyClips;
            }
            else
            {
                _logDestroyClips = logDestroyClips;
            }
        }
        /// <summary>
        /// Инициализация звуков и вибрации на нужные игровые события
        /// </summary>
        private void OnEnable()
        {
            Vibration.Init();
            _audioSource = GetComponent<AudioSource>();
            Events.OnKnifeDrop.AddListener(()=>
            {
                PlayClip(knifeDropClips);
                if(_vibration) Vibration.Vibrate(400);
            });
            Events.OnKnifeHit.AddListener(()=>
            {
                PlayClip(_logHitClips);
                if(_vibration) Vibration.VibratePop();
            });
            Events.OnWinGame.AddListener(()=>
            {
                PlayClip(_logDestroyClips);
                if(_vibration) Vibration.Vibrate(400);
            });
            Events.OnSettingsChange.AddListener(() =>
            {
                _audioSource.volume =  SaveManager.Inst.Sound.Volume;
                _vibration =  SaveManager.Inst.Sound.Vibration;
            });
            Events.OnAppleHit.AddListener(()=>PlayClip(appleHitClips));
            Events.OnThrow.AddListener(()=> PlayClip(onThrowClips));
            Events.OnClickButton.AddListener(()=> PlayClip(buttonClick));
            Events.OnUnlock.AddListener(()=> PlayClip(unlockKnifeClip));
            Events.OnEquip.AddListener((item)=>PlayClip(equipKnifeClip));
        }

        private void Start()
        {
            _audioSource.volume =  SaveManager.Inst.Sound.Volume;
            _vibration =  SaveManager.Inst.Sound.Vibration;
        }
        /// <summary>
        /// Проигрывание случайного клипа из списка
        /// </summary>
        /// <param name="clips"></param>
        private void PlayClip(List<AudioClip> clips) => _audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
        /// <summary>
        /// проигрывание клипа
        /// </summary>
        /// <param name="clip"></param>
        private void PlayClip(AudioClip clip) => _audioSource.PlayOneShot(clip);
    }
}