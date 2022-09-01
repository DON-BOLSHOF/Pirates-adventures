using System;
using PixelCrew.Model.Definition;
using PixelCrew.Model.Definition.Repositories.Items;
using PixelCrew.Model.Definition.Repository;
using UnityEngine;

namespace Model.Definition.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Potions", fileName = "Potions")]
    public class PotionRepository : DefRepository<PotionDef>
    {
        
    }

    [Serializable]
    public struct PotionDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private PotionEffects _effects;
        [SerializeField] private float _value;
        [SerializeField] private float _time;
        
        public string Id => _id;
        public PotionEffects Effects => _effects;
        public float Value => _value;
        public float Time => _time;
    }

    public enum PotionEffects
    {
        addHP,
        addSpeed
    }
}