using System.IO;
using UnityEditor;
using Systems.ItemSystem.Database;

namespace Systems.ItemSystem.Editor
{
    public class ItemTypeGenerator
    {
        const string defaultFileName = "ItemType.cs";
        static public void CheckAndGenerateFile()
        {
            string assetPath = GetAssetPathForFile(defaultFileName);
            if (string.IsNullOrEmpty(assetPath))
            {
                string getAssetPath = GetAssetPathForFile("ItemTypeGenerator.cs");
                assetPath = getAssetPath.Replace("Editor/ItemTypeGenerator.cs", defaultFileName);
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
                file.WriteLine("/// Generated Enum that can be used to easily select a ItemType from the ItemTypeDatabase.");
                file.WriteLine("/// The value assigned to each enum key is the ID of that ItemType within the ItemTypeDatabase");
                file.WriteLine("/// </summary>");

                file.WriteLine("namespace Systems.ItemSystem");
                file.WriteLine("{");
                file.WriteLine("\tpublic enum ItemType\n\t{");
                file.WriteLine("\t\tNone = 0,");

                for (int i = 0; i < ItemTypeDatabase.Instance.Count; i++)
                {
                    var entityType = ItemTypeDatabase.GetAt(i);
                    if (!string.IsNullOrEmpty(entityType.Name))
                    {
                        if (i == (ItemTypeDatabase.Instance.Count - 1))
                        {
                            file.WriteLine(string.Format("\t\t{0} = {1}", entityType.Name.Replace(" ", string.Empty), entityType.ID));
                        }
                        else
                        {
                            file.WriteLine(string.Format("\t\t{0} = {1},", entityType.Name.Replace(" ", string.Empty), entityType.ID));
                        }

                    }
                }

                file.WriteLine("\t}\n}");
            }
            AssetDatabase.Refresh();
        }
    }
}