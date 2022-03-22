namespace Knife
{
    /// <summary>
    /// Перечисление состояний ножика
    /// </summary>
    public enum KnifeState
    {
        Ready, // готов к запуску
        Moving, // запущен и летит к бревну
        Stopped, // остановлен (находится в бревне)
        Dropped // ударился о другой нож
    }
}