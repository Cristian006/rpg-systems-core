using UnityEditor;
using System.IO;
using Systems.ItemSystem.Database;

namespace Systems.ItemSystem.Editor
{
    public class ThrowableTypeGenerator
    {
        const string defaultFileName = "ThrowableType.cs";
        static public void CheckAndGenerateFile()
        {
            string assetPath = GetAssetPathForFile(defaultFileName);
            if (string.IsNullOrEmpty(assetPath))
            {
                string getAssetPath = GetAssetPathForFile("ThrowableTypeGenerator.cs");
                assetPath = getAssetPath.Replace("Editor/ThrowableTypeGenerator.cs", defaultFileName);
            }
            WriteThrowableTypesToFile(assetPath);
        }

        static string GetAssetPathForFile(string name)
        {
            string[] assetPaths = AssetDatabase.GetAllAssetPaths();
            foreach(var path in assetPaths)
            {
                if (path.Contains(name))
                {
                    return path;
                }
            }
            return string.Empty;
        }

        public static void WriteThrowableTypesToFile(string filepath)
        {
            using (StreamWriter file = File.CreateText(filepath)){
                file.WriteLine("/// <summary>");
                file.WriteLine("/// THIS IS A GENERATED FILE");
                file.WriteLine("/// ANY EDITS WILL BE DELETED ON NEXT GENERATION");
                file.WriteLine("/// Generated Enum that can be used to easily select a ThrowableType from the ThrowableTypeDatabase.");
                file.WriteLine("/// The value assigned to each enum key is the ID of that statType within the ThrowableTypeDatabase");
                file.WriteLine("/// </summary>");

                file.WriteLine("namespace Systems.ItemSystem");
                file.WriteLine("{");
                file.WriteLine("\tpublic enum ThrowableType\n\t{");
                file.WriteLine("\t\tNone = 0,");

                for (int i = 0; i < ThrowableTypeDatabase.Instance.Count; i++)
                {
                    var statType = ThrowableTypeDatabase.GetAt(i);
                    if (!string.IsNullOrEmpty(statType.Name))
                    {
                        if(i == (ThrowableTypeDatabase.Instance.Count - 1))
                        {
                            file.WriteLine(string.Format("\t\t{0} = {1}", statType.Name.Replace(" ", string.Empty), statType.ID));
                        }
                        else
                        {
                            file.WriteLine(string.Format("\t\t{0} = {1},", statType.Name.Replace(" ", string.Empty), statType.ID));
                        }
                        
                    }
                }

                file.WriteLine("\t}\n}");
            }
            AssetDatabase.Refresh();
        }

    }
}

