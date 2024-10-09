using UnityEngine;

namespace Game
{
    public class InputManager : MonoBehaviour
    {
        public Vector2 SwipeDelta { get; private set; }

        private Vector2 _startTouchPosition;
        private Vector2 _currentTouchPosition;

        private void Update()
        {
            SwipeDelta = Vector2.zero;

            HandleMobileDelta();
            HandlePCDelta();
        }

        private void HandleMobileDelta()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                    _startTouchPosition = touch.position;

                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    _currentTouchPosition = touch.position;
                    SwipeDelta = _currentTouchPosition - _startTouchPosition;
                    _startTouchPosition = _currentTouchPosition;
                }
            }
        }

        private void HandlePCDelta()
        {
            if (Input.GetMouseButtonDown(0))
                _startTouchPosition = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                _currentTouchPosition = Input.mousePosition;
                SwipeDelta = (Vector2)_currentTouchPosition - _startTouchPosition;
                _startTouchPosition = _currentTouchPosition;
            }
        }
    }
}
