using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EndlessRunner
{
    public class GameplayScreenHandler : UiScreens
    {
        [SerializeField] private Button backBtn;
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Toggle toggleSound;

        public Action OnGameStart;

        private void Start()
        {
            OnGameStart?.Invoke();
            toggleSound.onValueChanged.AddListener(SoundBehaviour);
            backBtn.onClick.AddListener(OnBackButtonClick);
            scoreText.text = $"Score : {0}";
        }

        private void SoundBehaviour(bool isOn)
        {
            if (isOn)
            {
                AudioManager.Instance.UnMute();
            }
            else
            {
                AudioManager.Instance.Mute();
            }
        }

        public void OnBackButtonClick()
        {
            GameConstants.currentGameState = GameState.Pause;
            UIScreenManager.Instance.ShowScreen(UiScreen.QuitPopup, UiScreenShowBehaviour.KeepPrevious);
        }

        internal void UpdateScore(int coinCount)
        {
            scoreText.text = $"Score : {coinCount}";
        }
    }
}
