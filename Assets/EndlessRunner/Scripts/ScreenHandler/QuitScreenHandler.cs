using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EndlessRunner
{
    public class QuitScreenHandler : UiScreens
    {
        [SerializeField] private Button yesBtn;
        [SerializeField] private Button noBtn;

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
            yesBtn.onClick.AddListener(OnYesButtonClick);
            noBtn.onClick.AddListener(OnNoButtonClick);
        }

        private void OnYesButtonClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnNoButtonClick()
        {
            rectTransform
                .DOScale(Vector3.zero, duration)
                .SetEase(Ease.InBack)
                .SetUpdate(true)
                .OnComplete(() =>
                {
                    GameConstants.currentGameState = GameState.Resume;
                    UIScreenManager.Instance.HideUIScreens(UiScreen.QuitPopup);
                });
        }
    }
}
