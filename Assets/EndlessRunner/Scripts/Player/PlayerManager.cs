using UnityEngine;
using System;
using System.Collections;

namespace EndlessRunner
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameplayScreenHandler gameplayScreenHandler;
        [SerializeField] private GameoverScreenHandler gameoverScreenHandler;

        [Header("Animation")]
        [SerializeField] private Animator animator;

        [SerializeField] private int lives = 1;

        public static Action OnGameOver;

        private int coinCount = 0;

        private static readonly int DeathHash = Animator.StringToHash("Death");
        private static readonly int RunHash = Animator.StringToHash("Run");

        private void OnEnable()
        {
            gameplayScreenHandler.OnGameStart += StartGame;
        }

        private void OnDisable()
        {
            gameplayScreenHandler.OnGameStart -= StartGame;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                lives--;
                if (lives <= 0)
                {
                    AudioManager.Instance.PlayOneShot("HurtSound");
                    GameConstants.currentGameState = GameState.Pause;
                    animator.SetTrigger(DeathHash);
                    StartCoroutine(GameOverPopup());
                }
            }
        }

        private IEnumerator GameOverPopup()
        {
            gameoverScreenHandler.DisplayFinalScore(coinCount);
            yield return new WaitForSeconds(1.5f);
            UIScreenManager.Instance.ShowScreen(UiScreen.Gameover, UiScreenShowBehaviour.HidePrevious);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                coinCount++;
                AudioManager.Instance.PlayOneShot("CoinCollect");
                other.gameObject.SetActive(false);
                gameplayScreenHandler.UpdateScore(coinCount);
            }
        }

        private void StartGame()
        {
            animator.SetTrigger(RunHash);
        }
    }
}
