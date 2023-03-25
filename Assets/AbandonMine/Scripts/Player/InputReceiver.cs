using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbandonMine.Player
{
    public class InputReceiver : MonoBehaviour
    {
        [SerializeField]
        private string horizontalAxisName = "Horizontal";
        
        [SerializeField]
        private string verticalAxisName = "Vertical";

        [SerializeField]
        private string mouseXAxisName = "Mouse X";
        
        [SerializeField]
        private string mouseYAxisName = "Mouse Y";

        [SerializeField]
        private KeyCode jumpingKey = KeyCode.Space;


        public float HorizontalAxisValue => Input.GetAxisRaw(horizontalAxisName);
        public float VerticalAxisValue => Input.GetAxisRaw(verticalAxisName);

        public float MouseXAxisValue => Input.GetAxisRaw(mouseXAxisName);
        public float MouseYAxisValue => Input.GetAxisRaw(mouseYAxisName);

        public bool WasJumpKeyPressed => Input.GetKeyDown(jumpingKey);
    }
}
