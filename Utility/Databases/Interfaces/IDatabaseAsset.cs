using UnityEngine;
using System.Collections;

namespace Systems.Utility.Database.Interfaces
{
    public interface IDatabaseAsset
    {
        int ID { get; set; }
        string Name { get; set; }
    }
}