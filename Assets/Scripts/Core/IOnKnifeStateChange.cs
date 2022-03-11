using Knife;

namespace Core
{
    public interface IOnKnifeStateChange
    {
        public void OnStateChange(KnifeState newState);
    }
}