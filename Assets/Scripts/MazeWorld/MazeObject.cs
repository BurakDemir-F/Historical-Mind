using UnityEngine;

namespace MazeWorld
{
    public abstract class MazeObject : MonoBehaviour,IPickable,IInteractable
    {
        public virtual void Pick()
        {
        }

        public virtual void Drop()
        {
        }

        public virtual void Interact()
        {
        }
    }
}
