using System.Collections.Generic;
using Core;
using Management;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// Класс, хранящий в себе изменяемые данные игрока
    /// </summary>
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private float unlockForBossDelay = 1f;
        private UserData _userData;
        public KnifeData Knife;
        public ShopData Shop;
        public SoundData Sound;
        public ScoreData Score;
        public static SaveManager Inst;
        public void Save() => SaveSystem.Save(_userData);
        /// <summary>
        /// Метод инииализации вспомогательных классов. Здесь проводится проверка на
        /// компоненты-дубликаты и на существование файла сохранения
        /// </summary>
        private void Awake()
        {
            if(!CheckForDublicates()) return;
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
        private void OnEnable()
        {
            Events.OnEquip.AddListener(Knife.Equip);
            Events.OnDefeatBoss.AddListener(()=>Invoke(nameof(UnlockForBoss), unlockForBossDelay));
        }
        private void Start()
        {
            var shopManager = FindObjectOfType<ShopManager>(true);
            Shop.Knives = shopManager.KnifeItems;
            foreach (var knifeItem in shopManager.KnifeItems)
            {
                if (knifeItem.Id != _userData.EquippedKnifeId) continue;
                Knife.CurrentSprite = knifeItem.KnifeSprite;
            }
        }
        /// <summary>
        /// Проверка на аналогичный компонент на сцене. Если он уже существует,
        /// то данный игровой объект уничтожается
        /// </summary>
        private bool CheckForDublicates()
        {
            if (Inst != null)
            {
                Destroy(gameObject);
                return false;
            }
            else
            {
                Inst = this;
                DontDestroyOnLoad(gameObject);
                return true;
            }
        }
        /// <summary>
        /// метод задержки события открытия ножа на уровне с боссом
        /// </summary>
        private void UnlockForBoss() => Shop.UnlockBossKnife();
    }
}