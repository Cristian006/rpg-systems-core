using Systems.Utility.Database;

namespace Systems.ItemSystem.Database
{
    public class ItemTypeDatabase : BaseDatabase<ItemTypeAsset>
    {
        const string DatabasePath = @"Resources/Systems/ItemSystem/Database/";
        const string DatabaseName = @"ItemTypeDatabase.asset";

        private static ItemTypeDatabase _instance = null;

        public static ItemTypeDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GetDatabase<ItemTypeDatabase>(DatabasePath, DatabaseName);
                }
                return _instance;
            }

            set
            {
                _instance = value;
            }
        }

        static public ItemTypeAsset GetAt(int index)
        {
            return Instance.GetAtIndex(index);
        }

        static public ItemTypeAsset GetAsset(int id)
        {
            return Instance.GetByID(id);
        }

        static public int GetAssetCount()
        {
            return Instance.Count;
        }
    }
}