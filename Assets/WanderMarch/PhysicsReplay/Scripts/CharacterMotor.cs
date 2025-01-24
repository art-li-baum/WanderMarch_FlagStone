using System;
using UnityEngine;
using UnityEngine.Serialization;
using WanderMarch.Scripts.Math;

namespace WanderMarch.PhysicsReplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterMotor : MonoBehaviour
    {
        private Rigidbody2D _body;

        private Vector2 _inputDirection;

        [Header("Parameters")] [SerializeField]
        private float maxSpeed = 10f;

        [SerializeField, Range(1f, 25f), Tooltip("How 'steep' the acceleration curve is")]
        private float maxAccelerationFactor = 16f;

        [SerializeField, Range(1f, 25f), Tooltip("The 'slide' factor when stopping")]
        private float maxDecelerationFactor = 20f;

        [SerializeField, Range(1f, 25f), Tooltip("How fast the motor accelerates/decelerates")]
        private float jerk;

        [SerializeField] private AnimationCurve TurnAroundFactor;

        [Header("Current Stats")] [SerializeField]
        private bool visualizeVelocityVectors = false;

        [SerializeField, ShowOnly] private Vector2 velocity;

        [SerializeField, ShowOnly] private float accelerationFactor;
        [SerializeField, ShowOnly] private Vector2 goalVelocity;

        public void SetInput(Vector2 input)
        {
            _inputDirection = input;

            //TODO Messaging?
        }

        // Start is called before the first frame update
        private void Start()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            goalVelocity = _inputDirection * maxSpeed;

            //Can give more power for sharp turns to create snappier controls
            var turnBoost = TurnAroundFactor.Evaluate(Vector2.Dot(goalVelocity.normalized, velocity.normalized));

            accelerationFactor = MathUtil.FloatDecay(accelerationFactor, //Decelerate if no input
                Mathf.Abs(goalVelocity.magnitude) > 0 ? maxAccelerationFactor * turnBoost : maxDecelerationFactor, jerk,
                GameTime.FixedScaledTime);


            //Update the velocity by applying acceleration
            velocity = MathUtil.Vector2Decay(velocity, goalVelocity, accelerationFactor, GameTime.FixedScaledTime);


            //Update position by applying velocity
            _body.position += (velocity * GameTime.FixedScaledTime);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!visualizeVelocityVectors) return;
            Vector3 vel = velocity;
            Vector3 gvel = goalVelocity;
            var position = transform.position;

            Debug.DrawLine(position, position + vel, Color.blue);
            Debug.DrawLine(position, position + gvel, Color.green);
        }
#endif
    }
}