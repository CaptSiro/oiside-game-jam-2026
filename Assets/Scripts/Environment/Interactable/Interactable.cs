using UnityEngine;

namespace Environment.Interactable
{
    public class Interactable : MonoBehaviour
    {
        public enum InteractableTypeEnum
        {
            NewItem,
            NPC,
            Puzzle,
            BookPickUp,
            Clue
        }

        [SerializeField] protected InteractableTypeEnum interactableType;

        public InteractableTypeEnum InteractableType => interactableType;
    }
}
