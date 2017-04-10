using UnityEngine;
namespace Systems.EntitySystem
{
    //Entity is simply a script that has an associated StatCollection
    //Gives children a method to get the stats it needs
    public abstract class Entity : MonoBehaviour
    {
        protected EntityData data;

        public EntityData Data
        {
            get
            {
                if (data == null)
                {
                    data = new EntityData();
                }
                return data;
            }
            set
            {
                data = value;
            }
        }
    }
}