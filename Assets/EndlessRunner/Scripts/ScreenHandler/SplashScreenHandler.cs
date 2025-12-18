using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunner
{
    public class SplashScreenHandler : UiScreens
    {
        [SerializeField] private Button playButton;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private Ease ease = Ease.OutBack;

        private void OnEnable()
        {
            rectTransform
                .DOScale(Vector3.one, duration)
                .SetEase(ease)
                .SetUpdate(true);
        }

        private void Start()
        {
            playButton.onClick.AddListener(OnPlayButtonClick);
            AudioManager.Instance.Play("BG Music");
        }

        private void OnPlayButtonClick()
        {
            UIScreenManager.Instance.ShowScreen(UiScreen.Gameplay, UiScreenShowBehaviour.HidePrevious);
            GameConstants.currentGameState = GameState.Resume;
        }
    }
}
