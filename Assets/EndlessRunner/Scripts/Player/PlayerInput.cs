using System;
using UnityEngine;

namespace EndlessRunner
{
    public class PlayerInput : MonoBehaviour
    {
        public Action OnLeft;
        public Action OnRight;
        public Action OnUp;
        public Action OnDown;

        [Header("Swipe")]
        [SerializeField] private float swipeThreshold = 50f;
        private Vector2 startPos;

        private void Update()
        {
            HandleKeyboard();
            HandleSwipe();
        }

        private void HandleKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                OnLeft?.Invoke();

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                OnRight?.Invoke();

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                OnUp?.Invoke();

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                OnDown?.Invoke();
        }

        private void HandleSwipe()
        {
            if (Input.GetMouseButtonDown(0))
                startPos = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 delta = (Vector2)Input.mousePosition - startPos;
                if (delta.magnitude < swipeThreshold) return;

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (delta.x > 0) OnRight?.Invoke();
                    else OnLeft?.Invoke();
                }
                else
                {
                    if (delta.y > 0) OnUp?.Invoke();
                    else OnDown?.Invoke();
                }
            }
        }
    }
}
