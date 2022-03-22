namespace Knife
{
    /// <summary>
    /// Интерфейс реагирование на изменение состояния ножика
    /// </summary>
    public interface IOnKnifeStateChange
    {
        public void OnStateChange(KnifeState newState);
    }
}