using System.IO;
using UnityEditor;
using Systems.EntitySystem.Database;

namespace Systems.EntitySystem.Editor
{
    public class EntityTypeGenerator
    {
        const string defaultFileName = "EntityType.cs";
        static public void CheckAndGenerateFile()
        {
            string assetPath = GetAssetPathForFile(defaultFileName);
            if (string.IsNullOrEmpty(assetPath))
            {
                string getAssetPath = GetAssetPathForFile("EntityTypeGenerator.cs");
                assetPath = getAssetPath.Replace("Editor/EntityTypeGenerator.cs", defaultFileName);
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
                file.WriteLine("/// Generated Enum that can be used to easily select a EntityType from the EntityTypeDatabase.");
                file.WriteLine("/// The value assigned to each enum key is the ID of that EntityType within the EntityTypeDatabase");
                file.WriteLine("/// </summary>");

                file.WriteLine("namespace Systems.EntitySystem.Enumerations");
                file.WriteLine("{");
                file.WriteLine("\tpublic enum EntityType\n\t{");
                file.WriteLine("\t\tNone = 0,");

                for (int i = 0; i < EntityTypeDatabase.Instance.Count; i++)
                {
                    var entityType = EntityTypeDatabase.GetAt(i);
                    if (!string.IsNullOrEmpty(entityType.Name))
                    {
                        if (i == (EntityTypeDatabase.Instance.Count - 1))
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