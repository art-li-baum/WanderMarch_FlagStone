using System;
using Unity.VisualScripting;
using UnityEngine;

namespace WanderMarch.Scripts
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterAI : MonoBehaviour
    {
        private CharacterMotor _motor;
        
        [SerializeField] private float lapTimer = 1f;
        private int _lap = 0;
        private float _l = 0;
        
        private enum State
        {
            Laps,
        }

       [SerializeField,ShowOnly] private State _currentState = State.Laps;
        
        // Start is called before the first frame update
        void Start()
        {
            _motor = GetComponent<CharacterMotor>();
        }

        // Update is called once per frame
        public void Update()
        {
            switch (_currentState)
            {
                case State.Laps:
                {
                    _l += Time.deltaTime;
                    if (_l < lapTimer) break;
                    switch(_lap) 
                    {
                        case 0:
                            _motor.SetInput(Vector2.down);
                            _lap = 1;
                            _l = 0;
                            break;
                        case 1:
                            _motor.SetInput(Vector2.left);
                            _lap = 2;
                            _l = 0;
                            break;
                        case 2:
                            _motor.SetInput(Vector2.up);
                            _lap = 3;
                            _l = 0;
                            break;
                        case 3:
                            _motor.SetInput(Vector2.right);
                            _lap = 0;
                            _l = 0;
                            break;
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ApplyInput()
        {
            
        }
    }
}
