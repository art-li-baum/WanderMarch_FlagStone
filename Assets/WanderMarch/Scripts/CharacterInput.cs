using System;
using UnityEngine;

using WanderMarch.Scripts.App;


namespace WanderMarch.Scripts
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterInput : MonoBehaviour
    {
        private VectorAction _moveInput;
        private CharacterMotor _motor;

        private void Start()
        {
            //TODO TEMP
            InputManager.Instance.SwapInputMaps("Character");

            _moveInput = InputManager.Instance.GetInput("Character", "Move") as VectorAction;
            _motor = GetComponent<CharacterMotor>();
        }

        // Update is called once per frame
        void Update()
        {
            _motor.SetInput(_moveInput.Value);
        }
    }
}
