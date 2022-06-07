using UnityEngine;

namespace MazeWorld
{
    public class MazeDecalPick : MonoBehaviour,IPickable
    {
        [SerializeField] private GameObject decalPrefab;
        
        public void Pick()
        {
        }

        public void Drop()
        {
            //var isHit = Physics.Raycast()
        }
    }
}
