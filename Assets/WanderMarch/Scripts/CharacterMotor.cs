using System;
using UnityEngine;
using UnityEngine.Serialization;
using WanderMarch.Scripts.Math;

namespace WanderMarch.Scripts
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMotor : MonoBehaviour
    {
        private Rigidbody2D _body;

        private Vector2 _inputDirection;

        [SerializeField] private float maxSpeed = 10f;
        [SerializeField, ShowOnly] private Vector2 velocity;
        [field: SerializeField, Range(1f,25f)] private float AccelerationFactor { get; set; }


        private Vector2 goalVelocity;

        // Start is called before the first frame update
        void Start()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        public void SetInput(Vector2 input)
        {
            _inputDirection = input;
            
            //TODO Messaging?
        }

        private void FixedUpdate()
        {
            goalVelocity = _inputDirection * maxSpeed;
            
            //Update the velocity by applying acceleration
            velocity = MathUtil.Vector2Decay(velocity, goalVelocity, AccelerationFactor , Time.fixedDeltaTime);
            

            //Update position by applying velocity
            _body.position += (velocity * Time.fixedDeltaTime);
        }
    }
}