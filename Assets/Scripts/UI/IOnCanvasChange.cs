namespace UI
{
    public interface IOnCanvasChange
    {
        public void OnCanvasChange(CanvasType newType, float timeInSeconds = 0f);
    }
}