namespace LTD.ImpactSystem
{
    public struct Impact
    {
        private readonly float pureValue;
        private readonly ImpactSource impactSource;

        public float PureValue => pureValue;
        public float ModifiedValue { get; set; }
        public bool IsHealImpact => ModifiedValue > 0;

        public ImpactOwner ImpactOwner => impactSource.impactOwner;
        public ImpactSource ImpactSource => impactSource;

        public Impact(float pureValue, ImpactSource impactSource)
        {
            ModifiedValue = this.pureValue = pureValue;
            this.impactSource = impactSource;
        }
    }
}
