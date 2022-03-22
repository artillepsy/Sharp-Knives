namespace Scriptable
{
    /// <summary>
    /// Интерфейс, который позволяет компонентам реагировать на загрузку уровня
    /// </summary>
    public interface IOnLevelLoad
    {
        public void OnLevelLoad(Level level);
    }
}