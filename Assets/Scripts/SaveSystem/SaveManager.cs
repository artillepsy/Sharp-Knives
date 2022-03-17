using System.Collections.Generic;
using Core;
using UI;
using UnityEngine;

namespace SaveSystem
{
    public class SaveManager : MonoBehaviour
    {
        private UserData _userData;
        public KnifeData Knife;
        public ShopData Shop;
        public SoundData Sound;
        public ScoreData Score;
        /*
        private Sprite _currentKnifeSprite;
        public Sprite CurrentKnifeSprite => _currentKnifeSprite;
        public float Volume => _userData.Volume;
        public bool Vibration => _userData.Vibration;
        public int EquippedKnifeId => _userData.EquippedKnifeId;
        public int WinCount => _winCount;
        public int AppleCount => _userData.AppleCount;
        public int CurrentScore => _currentScore;
        public int HighScore => _userData.HighScore;
        public List<int> UnlockedIds => _userData.UnlockedKniveIds;
        */
        private void OnEnable()
        {
            Events.OnAppleHit.AddListener( () =>Score.IncrementApples());
            Events.OnKnifeHit.AddListener(() => Score.CurrentScore++);
            Events.OnWinGame.AddListener(() =>
            {
                Score.IncrementWins();
                SaveSystem.Save(_userData);
            });
            Events.OnKnifeDrop.AddListener(OnKnifeDrop);
        }
        private void Awake()
        {
            Test_ClearProgress();
            _userData = SaveSystem.Load();
            if (_userData == null)
            {
                _userData = new UserData(0, 1, new List<int>(){1});
            }
            Knife = new KnifeData(_userData);
            Shop = new ShopData(_userData);
            Sound = new SoundData(_userData);
            Score = new ScoreData(_userData);
        }
        private void Start()
        {
            foreach (var knifeItem in FindObjectOfType<ShopManager>(true).KnifeItems)
            {
                if (knifeItem.Id != _userData.EquippedKnifeId) continue;
                Knife.CurrentSprite = knifeItem.KnifeSprite;
            }
        }
        private void Test_ClearProgress()
        {
            _userData = new UserData(100, 1, new List<int>(){1});
            SaveSystem.Save(_userData);
        }
        private void OnKnifeDrop()
        {
           // Debug.Log(Score.CurrentScore);
            if (_userData.HighScore < Score.CurrentScore)
            {
                _userData.HighScore = Score.CurrentScore;
                Debug.Log("high score: "+ _userData.HighScore);
            }
            Score.ResetWinCound();
            Score.CurrentScore = 0;
            SaveSystem.Save(_userData);
        }
    }
}