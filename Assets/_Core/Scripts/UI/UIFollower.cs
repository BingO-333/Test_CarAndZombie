using DG.Tweening;
using System;
using UnityEngine;

namespace Game
{
    public class UIFollower : MonoBehaviour
    {
        public bool IsShown => gameObject.activeSelf;

        public bool Offscreen { get; private set; }

        [SerializeField] private Vector2 _offset;
        [SerializeField] private bool _clampToTheCanvasBoundaries;

        [SerializeField] UpdateMethod _updateMethod;

        protected Transform _target { get; private set; }
        protected RectTransform _uiElement { get; private set; }

        private Camera _mainCamera;

        private Tweener _scaleTweener;

        protected virtual void Awake()
        {
            _mainCamera = Camera.main;

            _uiElement = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _uiElement.anchoredPosition = Vector3.zero;
        }

        protected virtual void Update()
        {
            if (_updateMethod != UpdateMethod.Update)
                return;

            UpdatePosition();
        }

        protected virtual void FixedUpdate()
        {
            if (_updateMethod != UpdateMethod.Fixed)
                return;

            UpdatePosition();
        }

        protected virtual void LateUpdate()
        {
            if (_updateMethod != UpdateMethod.Late)
                return;

            UpdatePosition();
        }

        public void SetFollowTarget(Transform target)
        {
            _target = target;
            UpdatePosition();
        }

        public void Show(Action onComplete = null)
        {
            if (IsShown)
                return;
            
            gameObject.SetActive(true);

            _scaleTweener.KillIfActiveAndPlaying();
            _scaleTweener = transform.DOScale(Vector3.one, 0.25f)
                .ChangeStartValue(Vector3.zero)
                .SetEase(Ease.OutBack)
                .OnComplete(() => onComplete?.Invoke());
        }

        public void Hide(Action onComplete = null)
        {
            _scaleTweener.KillIfActiveAndPlaying();
            _scaleTweener = transform.DOScale(Vector3.zero, 0.1f)
                .SetEase(Ease.InBack)
                .OnComplete(() => {
                    gameObject.SetActive(false);
                    onComplete?.Invoke();
                });
        }

        private void UpdatePosition()
        {
            if (_target == null || _mainCamera == null || _uiElement == null)
                return;
           
            Vector3 viewportPoint = _mainCamera.WorldToViewportPoint(_target.position);
            viewportPoint += new Vector3(_offset.x, _offset.y, 0);

            Offscreen = viewportPoint.x > 1f || viewportPoint.x < 0f ||
                        viewportPoint.y > 1f || viewportPoint.y < 0f;

            if (_clampToTheCanvasBoundaries)
            {
                if (viewportPoint.z < 0)
                    viewportPoint *= -1;

                viewportPoint.x = Mathf.Clamp(viewportPoint.x, 0.1f, 0.9f);
                viewportPoint.y = Mathf.Clamp(viewportPoint.y, 0.08f, 0.92f);
            }

            _uiElement.anchorMin = viewportPoint;
            _uiElement.anchorMax = viewportPoint;
        }

        [Serializable] private enum UpdateMethod
        {
            Update,
            Fixed,
            Late
        }
    }
}