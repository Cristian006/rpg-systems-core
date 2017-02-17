using Systems.ItemSystem.Interfaces;

namespace Systems.ItemSystem
{
    public class Throwable : Weapon, IThrowable
    {
        ThrowableType throwType;
        float force;

        public Throwable(WeaponAsset wa) : base(wa)
        {
            throwType = wa.TType;
            force = wa.Force;
        }
        

        public ThrowableType ThrowType
        {
            get
            {
                return throwType;
            }

            set
            {
                throwType = value;
            }
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
