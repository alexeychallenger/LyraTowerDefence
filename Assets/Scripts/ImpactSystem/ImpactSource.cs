namespace LTD.ImpactSystem
{
    public struct ImpactSource
    {
        public ImpactOwner impactOwner;

        public ImpactSource(ImpactOwner impactOwner)
        {
            this.impactOwner = impactOwner;
        }
    }
}
