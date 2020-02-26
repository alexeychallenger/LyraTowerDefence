namespace LTD.ImpactSystem
{
    public interface IImpactDataProvider
    {
        ImpactOwner ImpactOwner { get; }
        ImpactSource ImpactSource { get; }
    }
}
