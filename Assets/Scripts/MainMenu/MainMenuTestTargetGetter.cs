using MazeWorld.Npc;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuTestTargetGetter : MonoBehaviour,IGetTargetTransform
    {
        public Transform GetTarget()
        {
            return GameObject.FindWithTag("Player").transform;
        }
    }
}
