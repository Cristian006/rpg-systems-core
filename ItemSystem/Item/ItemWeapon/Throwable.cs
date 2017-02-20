using Systems.ItemSystem.Interfaces;

namespace Systems.ItemSystem
{
    public class Throwable : Weapon, IThrowable
    {
        float force;

        public Throwable(WeaponAsset wa) : base(wa)
        {
            force = wa.Force;
        }
        
        public float Force
        {
            get
            {
                return force;
            }

            set
            {
                force = value;
            }
        }
    }
}
