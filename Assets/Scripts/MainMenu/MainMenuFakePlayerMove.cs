using System;
using System.Collections;
using DG.Tweening;
using MazeWorld.Npc;
using UnityEngine;

namespace MainMenu
{
    public class MainMenuFakePlayerMove : MonoBehaviour
    {
        [SerializeField] private Transform target1,target2;
        [SerializeField] private bool isOnTarget1 = true;
        [SerializeField] private float movementTime = 0.5f;
        [SerializeField] private NpcRoomOperationsBehaviour npcRoomBehaviour;
        private Sequence _moveSequence;
        
        private void Start()
        {
            StartCoroutine(GoToTargetCor());
            
        }

        private void OnDestroy()
        {
            _moveSequence.Kill();
            StopCoroutine(GoToTargetCor());
        }

        private IEnumerator GoToTargetCor()
        {
            var wait3Sec = new WaitForSeconds(3f);
            var wait6Sec = new WaitForSeconds(6f);
            yield return wait3Sec;
            npcRoomBehaviour.TestOnRoomEnter();
            GoToTarget();

            var counter = 0;
            while (counter++ < 100)
            {
                GoToTarget();
                yield return wait6Sec;
            }
        }
        
        private void GoToTarget()
        {
            if (isOnTarget1)
            {
                _moveSequence = DOTween.Sequence();
                _moveSequence.Append(transform.DOMove(target2.position, movementTime));
                _moveSequence.OnComplete(() => _moveSequence.Kill());
                isOnTarget1 = false;
            }
            else
            {
                _moveSequence = DOTween.Sequence();
                _moveSequence.Append(transform.DOMove(target1.position, movementTime));
                _moveSequence.OnComplete(() => _moveSequence.Kill());
                isOnTarget1 = true;
            }
        
        }
    
    }
}
