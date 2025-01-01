using UnityEngine;

namespace Snap.Card
{
    [CreateAssetMenu(menuName = "Cards/Create Card Attributes")]
    public class Attributes : ScriptableObject
    {
        public enum CardID
        {
            Temp,
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
