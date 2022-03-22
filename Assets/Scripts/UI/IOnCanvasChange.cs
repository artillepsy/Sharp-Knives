namespace UI
{
    /// <summary>
    /// Интерфейс, нужный для реагирования скриптов на изменение окон
    /// </summary>
    public interface IOnCanvasChange
    {
        public void OnCanvasChange(CanvasType newType, float timeInSeconds = 0f);
    }
}