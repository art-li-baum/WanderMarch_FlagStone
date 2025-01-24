using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using WanderMarch.Scripts.App;


namespace WanderMarch.PhysicsReplay
{
    public class PhysicsReplayInputManager : Singleton<PhysicsReplayInputManager>
    {
        [SerializeField] private InputActionAsset physicsReplayActions;

        private InputInterpreter _terp;

        public VectorAction Move { private set; get; }

        private const string TOPDOWNMOVE = "Character";

        /// <summary>
        /// creates and populates input mapping before inputs are called in scene
        /// </summary>
        /// <exception cref="System.ArgumentNullException"></exception>
        protected override void Awake()
        {
            base.Awake();

            _terp = new InputInterpreter();


            if (physicsReplayActions == null)
            {
                throw new System.ArgumentNullException("Input Manager Does not have a map assinged");
            }

            _terp.Initialize(physicsReplayActions);

            //Make commonly used inputs readily available
            Move = _terp.GetInput(TOPDOWNMOVE, "Move") as VectorAction;
        }

        /// <summary>
        /// Allows swapping via specific enums for input action maps
        /// </summary>
        /// <param name="inputMode"></param>
        public void SwapInputMode(InputMode inputMode)
        {
            var modeString = "";

            switch (inputMode)
            {
                case InputMode.TopDownMove: modeString = TOPDOWNMOVE;

                    break;
            }

            _terp.SwapInputMaps(modeString);
        }

    }

    public enum InputMode
    {
        TopDownMove,
    };



}



