using Systems.Utility.Database;

namespace Systems.ItemSystem.Database
{
    public class WeaponTypeDatabase : BaseDatabase<WeaponTypeAsset>
    {
        const string DatabasePath = @"Resources/Systems/ItemSystem/Databases/";
        const string DatabaseName = @"WeaponTypeDatabase.asset";

        private static WeaponTypeDatabase _instance = null;

        public static WeaponTypeDatabase Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GetDatabase<WeaponTypeDatabase>(DatabasePath, DatabaseName);
                }
                return _instance;
            }

            set
            {
                _instance = value;
            }
        }

        static public WeaponTypeAsset GetAt(int index)
        {
            return Instance.GetAtIndex(index);
        }

        static public WeaponTypeAsset GetAsset(int id)
        {
            return Instance.GetByID(id);
        }

        static public int GetAssetCount()
        {
            return Instance.Count;
        }
    }
}