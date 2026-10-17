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
            BookPickUp
        }

        [SerializeField] private InteractableTypeEnum interactableType;

        public InteractableTypeEnum InteractableType => interactableType;
    }
}
