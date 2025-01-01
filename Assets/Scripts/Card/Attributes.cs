using UnityEngine;

namespace Snap.Card
{
    [CreateAssetMenu(menuName = "Cards/Create Card Attributes")]
    public class Attributes : ScriptableObject
    {
        public enum CardID
        {
            AbsorbingMan,
            Blade, Brood,
            CassandraNova, ColleenWing,
            Dracula,
            GhostRider, Gorr, Gambit, Grandmaster,
            Hazmat, Hela, Helicarrier,
            Ironheart, IronLad, IronMan,
            JaneFosterThor,
            LukeCage,
            Magick, MisterNegative, MODOK, MoonNight, Morbius, Mystique,
            Odin,
            Psylocke,
            RevonaRennslayer, Rogue,
            Sage, Sera, SilverSurfer, SuperSkrull,
            TaskMaster,
            Wong,
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
