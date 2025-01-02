using System;
using UnityEngine;

namespace Snap.Card
{
    [CreateAssetMenu(menuName = "Cards/Create Card Attributes")]
    public class AttributeObject : ScriptableObject
    {
        [field: SerializeField] public Attributes Data { get; private set; }
        public Attributes.CardID ID => Data.ID;
        public string Name => Data.Name;
        public Sprite BgSprite => Data.BgSprite;
        public Sprite MainSprite => Data.MainSprite;
        public int Energy => Data.Energy;
        public int Power => Data.Power;
        public string AbilityText => Data.AbilityText;
    }

    [Serializable]
    public class Attributes
    {
        public enum CardID
        {
            AbsorbingMan,
            Blade,
            Brood,
            CassandraNova,
            ColleenWing,
            Dracula,
            GhostRider,
            Gorr,
            Gambit,
            Grandmaster,
            Hazmat,
            Hela,
            Helicarrier,
            Ironheart,
            IronLad,
            IronMan,
            JaneFosterThor,
            LukeCage,
            Magik,
            MisterNegative,
            MODOK,
            MoonNight,
            Morbius,
            Mystique,
            Odin,
            Psylocke,
            RevonaRennslayer,
            Rogue,
            Sage,
            Sera,
            SilverSurfer,
            SuperSkrull,
            TaskMaster,
            Wong
        }

        [field: SerializeField] public CardID ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public Sprite BgSprite { get; private set; }
        [field: SerializeField] public Sprite MainSprite { get; private set; }
        [field: SerializeField] public int Energy { get; private set; }
        [field: SerializeField] public int Power { get; private set; }
        [field: SerializeField] public string AbilityText { get; private set; }
    }
}