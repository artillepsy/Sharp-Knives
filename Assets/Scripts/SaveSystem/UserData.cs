using System.Collections.Generic;

namespace SaveSystem
{
    [System.Serializable]
    public class UserData
    {
        public int AppleCount;
        public int HighScore;
        public int CurrentKnifeId;
        public List<int> UnlockedKniveIds;

        public float Volume = 0.5f;
        public bool Vibration = true;

        public UserData(int appleCount, int highScore, int currentKnifeId, List<int> unlockedKnives)
        {
            AppleCount = appleCount;
            HighScore = highScore;
            CurrentKnifeId = currentKnifeId;
            UnlockedKniveIds = unlockedKnives;
            Volume = 0.5f;
            Vibration = true;
        }
    }
}