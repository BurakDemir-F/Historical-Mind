using System;
using Maze;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace MazeWorld.Npc
{
    public class NpcBehaviour : MonoBehaviour
    {
        [SerializeField] private NpcRoomOperationsBehaviour roomOperationsBehaviour;
        [SerializeField] private LivingThing npcData;
        [SerializeField] private float attackRange;
        [SerializeField] private float speed;
        [SerializeField] private Transform target;
        public event Action OnAttackRange;
        public event Action OnChaseRange;
        public event Action OnPlayerEscape;

        private bool _isInRange = false;
        
        private void Start()
        {
            SetData();
            roomOperationsBehaviour.OnPlayerRoom += PlayerEnterRoomHandler;
            roomOperationsBehaviour.OnPlayerExitRoom += PlayerExitRoomHandler;
            OnAttackRange += OnAttackRangeHandler;
        }

        private void OnDestroy()
        {
            roomOperationsBehaviour.OnPlayerRoom -= PlayerEnterRoomHandler;
            roomOperationsBehaviour.OnPlayerExitRoom -= PlayerExitRoomHandler;
            OnAttackRange -= OnAttackRangeHandler;
        }


        private void PlayerEnterRoomHandler()
        {
            OnChaseRange?.Invoke();
            _isInRange = true;
        }

        private void PlayerExitRoomHandler()
        {
            OnPlayerEscape?.Invoke();
            _isInRange = false;
        }

        private void Update()
        {
            if(!_isInRange) return;

            var targetPos = GetTargetPos();
            var myPos = transform.position;
            var distanceVec = targetPos - myPos;
            var distanceLength = distanceVec.magnitude;

            if (distanceLength < attackRange) OnAttackRange?.Invoke();
            else MoveTowardsPlayer(targetPos,distanceVec);
        }

        private void MoveTowardsPlayer(Vector3 targetPos,Vector3 distanceVec)
        {
            transform.LookAt(targetPos);
            transform.Translate(distanceVec * speed * Time.deltaTime);
        }

        private Vector3 GetTargetPos()
        {
            target = PlayerInfo.PlayerTransform;
            return PlayerInfo.PlayerPosition;
        }

        public void SetData()
        {
            attackRange = npcData.AttackRange;
            speed = npcData.Speed;
        }

        #region Test

        private void OnAttackRangeHandler()
        {
            Debug.Log("on attack range!");
        }
        

        #endregion
    }
}
