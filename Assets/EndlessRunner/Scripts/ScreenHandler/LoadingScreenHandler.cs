using UnityEngine;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
using System.Threading;

namespace EndlessRunner
{
    public class LoadingScreenHandler : UiScreens
    {
        [Header("UI")]
        [SerializeField] private Image progressBar;
        [SerializeField] private TMP_Text progressText;

        [Header("Loading Settings")]
        [SerializeField] private float loadDuration = 2.5f;

        private CancellationTokenSource _cts;

        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
            StartLoading(_cts.Token).Forget();
        }

        private void OnDisable()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        private async UniTaskVoid StartLoading(CancellationToken token)
        {
            float elapsed = 0f;

            while (elapsed < loadDuration)
            {
                token.ThrowIfCancellationRequested();

                elapsed += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsed / loadDuration);

                UpdateUI(progress);

                await UniTask.Yield(PlayerLoopTiming.Update, token);
            }

            UpdateUI(1f);
            OnLoadingCompleted();
        }

        private void UpdateUI(float progress)
        {
            if (progressBar != null)
                progressBar.fillAmount = progress;

            if (progressText != null)
                progressText.text = $"Loading {Mathf.RoundToInt(progress * 100f)}%";
        }

        private void OnLoadingCompleted()
        {
            UIScreenManager.Instance.ShowScreen(UiScreen.Splash, UiScreenShowBehaviour.HidePrevious);
        }
    }
}
