using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EndlessRunner
{
    public class GameoverScreenHandler : UiScreens
    {
        [SerializeField] private Button restartBtn;
        [SerializeField] private TMP_Text bestScoreText;
        [SerializeField] private TMP_Text finalScoreText;

        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private Ease ease = Ease.OutBack;

        private GameData gameData;

        private void Awake()  
        {
            gameData = SaveManager.Load();
        }

        private void OnEnable()
        {
            rectTransform
                .DOScale(Vector3.one, duration)
                .SetEase(ease)
                .SetUpdate(true);
        }

        private void Start()
        {
            restartBtn.onClick.AddListener(OnRestartButtonClicked);
            UpdateBestScoreUI();
        }

        private void OnRestartButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        internal void DisplayFinalScore(int score)
        {
            if (gameData == null)  
                gameData = SaveManager.Load();

            gameData.lastScore = score;

            if (score > gameData.bestScore)
            {
                gameData.bestScore = score;
            }

            SaveManager.Save(gameData);

            finalScoreText.text = $"Score: {score}";
            bestScoreText.text = $"Best Score: {gameData.bestScore}";
        }

        private void UpdateBestScoreUI()
        {
            bestScoreText.text = $"Best Score: {gameData.bestScore}";
        }
    }
}
