using LTD.ActorSystem;

namespace LTD.ImpactSystem
{
    public struct ImpactOwner
    {
        public int id;
        public Actor actor;

        public ImpactOwner(Actor actor) : this()
        {
            this.id = actor.GetInstanceID();
            this.actor = actor;
        }
    }
}
