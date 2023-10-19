
using UnityEngine;
using Unity.Entities;
public class EntitySpawner : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref PrefabEntity enitity) => EntityManager.Instantiate(enitity.Prefabentity));
           
        
    }
}
