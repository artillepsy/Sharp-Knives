using System.Collections.Generic;

namespace Core
{
    [System.Serializable]
    public class UserData
    {
        public int AppleCount;
        public int HighScore;
        public int CurrentKnifeId;
        public List<int> UnlockedKniveIds;

        public UserData(int appleCount, int highScore, int currentKnifeId, List<int> unlockedKnives)
        {
            AppleCount = appleCount;
            HighScore = highScore;
            CurrentKnifeId = currentKnifeId;
            UnlockedKniveIds = unlockedKnives;
        }
    }
}