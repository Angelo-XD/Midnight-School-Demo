using UnityEngine;
using Unity.Entities;
using JetBrains.Annotations;

[GenerateAuthoringComponent]
public struct PrefabEntity : IComponentData
{
    
    public Entity Prefabentity;
}
