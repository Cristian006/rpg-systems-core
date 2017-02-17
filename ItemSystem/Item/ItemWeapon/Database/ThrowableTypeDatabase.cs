using Systems.Utility.Database;

namespace Systems.ItemSystem.Database
{
    public class ThrowableTypeDatabase : BaseDatabase<ThrowableTypeAsset>
    {
        const string DatabasePath = @"Resources/Systems/ItemSystem/Databases/";
        const string DatabaseName = @"ThrowableTypeDatabase.asset";

        private static ThrowableTypeDatabase _instance = null;

        public static ThrowableTypeDatabase Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = GetDatabase<ThrowableTypeDatabase>(DatabasePath, DatabaseName);
                }
                return _instance;
            }

            set
            {
                _instance = value;
            }
        }

        static public ThrowableTypeAsset GetAt(int index)
        {
            return Instance.GetAtIndex(index);
        }

        static public ThrowableTypeAsset GetAsset(int id)
        {
            return Instance.GetByID(id);
        }

        static public ThrowableTypeAsset GetByType(ThrowableType tT)
        {
            return Instance.GetByName(tT.ToString());
        }

        static public int GetAssetCount()
        {
            return Instance.Count;
        }
    }
}