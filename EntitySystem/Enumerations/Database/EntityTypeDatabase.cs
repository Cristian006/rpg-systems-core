using Systems.Utility.Database;

namespace Systems.EntitySystem.Database
{
    public class EntityTypeDatabase : BaseDatabase<EntityTypeAsset>
    {
        const string DatabasePath = @"Resources/Systems/EntitySystem/Database/";
        const string DatabaseName = @"EntityTypeDatabase.asset";

        private static EntityTypeDatabase _instance = null;

        public static EntityTypeDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GetDatabase<EntityTypeDatabase>(DatabasePath, DatabaseName);
                }
                return _instance;
            }

            set
            {
                _instance = value;
            }
        }

        static public EntityTypeAsset GetAt(int index)
        {
            return Instance.GetAtIndex(index);
        }

        static public EntityTypeAsset GetAsset(int id)
        {
            return Instance.GetByID(id);
        }

        static public int GetAssetCount()
        {
            return Instance.Count;
        }
    }
}