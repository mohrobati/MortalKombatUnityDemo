using System;
using Vector2 = UnityEngine.Vector2;

namespace DefaultNamespace
{
    public class PlayerStatus
    {
        private bool _isWalking;
        private bool _isSitted;
        private bool _isJumping;
        private bool _isPunching;
        private bool _isKicking;
        private bool _isFeelingDizzy;
        private bool _isDead;
        private bool _isDefending;
        private bool _isOnTheFloor;
        private bool _isAttacking;
        private bool _doingSkill;
        private bool _hasCollisionWithOpponent;
        private float _health;
        private Vector2 _velocity;
        /*private bool _isGetOverHereing;
        private bool _isFreezing;*/
        
        public bool IsWalking
        {
            get { return _isWalking; }
            set { _isWalking = value; }
        }

        public bool IsSitted
        {
            get { return _isSitted; }
            set { _isSitted = value; }
        }

        public bool IsJumping
        {
            get { return _isJumping; }
            set { _isJumping = value; }
        }

        public bool IsPunching
        {
            get { return _isPunching; }
            set { _isPunching = value; }
        }

        public bool IsKicking
        {
            get { return _isKicking; }
            set { _isKicking = value; }
        }

        public bool IsFeelingDizzy
        {
            get { return _isFeelingDizzy; }
            set { _isFeelingDizzy = value; }
        }

        public bool IsDead
        {
            get { return _isDead; }
            set { _isDead = value; }
        }

        public bool IsDefending
        {
            get { return _isDefending; }
            set { _isDefending = value; }
        }

        public bool IsOnTheFloor
        {
            get { return _isOnTheFloor; }
            set { _isOnTheFloor = value; }
        }

        public bool IsAttacking
        {
            get { return _isAttacking; }
            set { _isAttacking = value; }
        }

        public bool DoingSkill
        {
            get { return _doingSkill; }
            set { _doingSkill = value; }
        }

        public float Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public bool isHealthComplete()
        {
            return Math.Abs(_health - 100) < 0.01;
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public bool HasCollisionWithOpponent
        {
            get { return _hasCollisionWithOpponent; }
            set { _hasCollisionWithOpponent = value; }
        }
    }
}