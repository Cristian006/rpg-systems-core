using System.IO;
using UnityEditor;
using Systems.ItemSystem.Database;

namespace Systems.ItemSystem.Editor
{
    public class WeaponTypeGenerator
    {
        const string defaultFileName = "WeaponType.cs";
        static public void CheckAndGenerateFile()
        {
            string assetPath = GetAssetPathForFile(defaultFileName);
            if (string.IsNullOrEmpty(assetPath))
            {
                string getAssetPath = GetAssetPathForFile("WeaponTypeGenerator.cs");
                assetPath = getAssetPath.Replace("Editor/WeaponTypeGenerator.cs", defaultFileName);
            }
            WriteStatTypesToFile(assetPath);
        }

        static string GetAssetPathForFile(string name)
        {
            string[] assetPaths = AssetDatabase.GetAllAssetPaths();
            foreach (var path in assetPaths)
            {
                if (path.Contains(name))
                {
                    return path;
                }
            }
            return string.Empty;
        }

        public static void WriteStatTypesToFile(string filepath)
        {
            using (StreamWriter file = File.CreateText(filepath))
            {
                file.WriteLine("/// <summary>");
                file.WriteLine("/// THIS IS A GENERATED FILE");
                file.WriteLine("/// ANY EDITS WILL BE DELETED ON NEXT GENERATION");
                file.WriteLine("/// Generated Enum that can be used to easily select a WeaponType from the WeaponTypeDatabase.");
                file.WriteLine("/// The value assigned to each enum key is the ID of that WeaponType within the WeaponTypeDatabase");
                file.WriteLine("/// </summary>");

                file.WriteLine("namespace Systems.ItemSystem");
                file.WriteLine("{");
                file.WriteLine("\tpublic enum WeaponType\n\t{");
                file.WriteLine("\t\tNone = 0,");

                for (int i = 0; i < WeaponTypeDatabase.Instance.Count; i++)
                {
                    var entityType = WeaponTypeDatabase.GetAt(i);
                    if (!string.IsNullOrEmpty(entityType.Name))
                    {
                        if (i == (WeaponTypeDatabase.Instance.Count - 1))
                        {
                            file.WriteLine(string.Format("\t\t{0} = {1}", entityType.Name.Replace(" ", string.Empty), entityType.ID));
                        }
                        else
                        {
                            file.WriteLine(string.Format("\t\t{0} = {1},", entityType.Name.Replace(" ", string.Empty), entityType.ID));
                        }

                    }
                }

                file.WriteLine("\t}\n}\n");
            }
            AssetDatabase.Refresh();
        }
    }
}