namespace Core
{
    [System.Serializable]
    public class UserData
    {
        public int AppleCount;
        public int HighScore;
        public int CurrentKnifeId;
        public int[] UnlockedKniveIds;

        public UserData(int appleCount, int highScore, int currentKnifeId, int[] unlockedKnives)
        {
            AppleCount = appleCount;
            HighScore = highScore;
            CurrentKnifeId = currentKnifeId;
            UnlockedKniveIds = unlockedKnives;
        }
    }
}