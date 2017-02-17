using Systems.Utility.Database;

namespace Systems.ItemSystem.InventorySystem.Database
{
    public class InventoryDatabase : BaseDatabase<InventoryAsset>
    {
        const string DatabasePath = @"Resources/Systems/ItemSystem/Databases/";
        const string DatabaseName = @"InventoryDatabase.asset";

        private static InventoryDatabase _instance = null;

        public static InventoryDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GetDatabase<InventoryDatabase>(DatabasePath, DatabaseName);
                }
                return _instance;
            }
        }

        public static bool ContainsAsset(string name)
        {
            return Instance.Contains(name);
        }

        public static bool ContainsAsset(int id)
        {
            return Instance.Contains(id);
        }

        static public InventoryAsset GetAt(int index)
        {
            return Instance.GetAtIndex(index);
        }

        static public InventoryAsset GetAsset(int id)
        {
            return Instance.GetByID(id);
        }

        static public InventoryAsset GetAsset(string name)
        {
            return Instance.GetByName(name);
        }

        static public int GetAssetCount()
        {
            return Instance.Count;
        }
    }
}