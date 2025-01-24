using System;
using UnityEngine;

using WanderMarch.Scripts.App;



namespace WanderMarch.PhysicsReplay
{
    [RequireComponent(typeof(CharacterMotor))]
    public class TopDownPlayerInput : MonoBehaviour
    {
        private VectorAction _moveInput;
        private CharacterMotor _motor;

        private void Start()
        {
            //TODO TEMP
            PhysicsReplayInputManager.Instance.SwapInputMode(InputMode.TopDownMove);

            _moveInput = PhysicsReplayInputManager.Instance.Move;
            _motor = GetComponent<CharacterMotor>();
        }

        // Update is called once per frame
        void Update()
        {
            _motor.SetInput(_moveInput.Value);
        }
    }
}
