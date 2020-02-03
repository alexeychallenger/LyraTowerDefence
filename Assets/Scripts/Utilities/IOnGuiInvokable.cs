namespace LTD.Utilities
{
    public interface IOnGuiInvokable
    {
        void OnGui();
        bool DrawGui { get; }
    }
}