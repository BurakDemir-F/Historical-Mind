using System;
using System.Collections;
using Maze;
using ScriptableObjects;
using UnityEngine;
using AYellowpaper;
namespace MazeWorld.Npc
{
    public class NpcBehaviour : MonoBehaviour
    {
        [SerializeField] private NpcRoomOperationsBehaviour roomOperationsBehaviour;
        [SerializeField] private LivingThing npcData;
        [SerializeField] private float attackRange;
        [SerializeField] private float speed;
        [SerializeField] private Transform target;
        [SerializeField] private InterfaceReference<IGetTargetTransform> targetPosGetter;
        public event Action OnAttackRange;
        public event Action OnChaseRange;
        public event Action OnPlayerEscape;

        private bool _isInRange = false;
        private Vector3 _startPos;
        
        private void Start()
        {
            SetData();
            roomOperationsBehaviour.OnPlayerRoom += PlayerEnterRoomHandler;
            roomOperationsBehaviour.OnPlayerExitRoom += PlayerExitRoomHandler;
        }

        private void OnDestroy()
        {
            roomOperationsBehaviour.OnPlayerRoom -= PlayerEnterRoomHandler;
            roomOperationsBehaviour.OnPlayerExitRoom -= PlayerExitRoomHandler;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void PlayerEnterRoomHandler()
        {
            OnChaseRange?.Invoke();
            _isInRange = true;
            StopAllCoroutines();
        }

        private void PlayerExitRoomHandler()
        {
            OnPlayerEscape?.Invoke();
            _isInRange = false;
            StartCoroutine(MoveToTarget(_startPos));
        }

        private void Update()
        {
            if(!_isInRange) return;
            
            var targetPos = GetTargetPos();
            var myPos = transform.position;
            var distanceVec = targetPos - myPos;
            var distanceLength = distanceVec.magnitude;
        
            if (distanceLength < attackRange) 
                OnAttackRange?.Invoke();
            else
            {
                OnChaseRange?.Invoke();
                MoveTowardsTarget(targetPos);
            }
        }

        protected virtual void MoveTowardsTarget(Vector3 targetPos)
        {
            var myPos = transform.position;
            var positionY = myPos.y;
            targetPos = new Vector3(targetPos.x, positionY, targetPos.z);
            var distanceVec = targetPos - myPos;
            
            transform.Translate(distanceVec.normalized * speed * Time.deltaTime, Space.World);
            transform.LookAt(targetPos);
        }

        private Vector3 GetTargetPos()
        {
            target = targetPosGetter.Value.GetTarget();
            return target.position;
        }

        private void SetData()
        {
            attackRange = npcData.AttackRange;
            speed = npcData.Speed;
            _startPos = transform.position;
        }

        private IEnumerator MoveToTarget(Vector3 targetPos)
        {
            var wait = new WaitForEndOfFrame();
            while ((targetPos - transform.position).magnitude > .1f)
            {
                MoveTowardsTarget(targetPos);
                yield return wait;
            }
        }
        
        #region Test

        private void OnAttackRangeHandler()
        {
            Debug.Log("on attack range!");
        }
        

        #endregion
    }
}
