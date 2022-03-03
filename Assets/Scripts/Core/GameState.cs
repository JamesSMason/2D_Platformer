using System;
using System.Collections;
using UnityEngine;

namespace MM.Core
{
    public class GameState : MonoBehaviour
    {
        [SerializeField] int scoreForExtraLife = 20000;
        [SerializeField] int scorePerAirUnit = 10;
        [SerializeField] float delay = 0.01f;

        bool playGame = true;

        Timer timer = null;
        Score score = null;
        Lives lives = null;
        LazyValue<LevelLoader> levelLoader = null;
        LazyValue<SavingWrapper> savingWrapper = null;

        public Action OnScoreChanged;
        public Action OnLivesChanged;
        public Action OnSliderChanged;

        void Awake()
        {
            timer = GetComponent<Timer>();
            score = FindObjectOfType<Score>();
            lives = FindObjectOfType<Lives>();
            levelLoader = new LazyValue<LevelLoader>(GetLevelLoader);
            savingWrapper = new LazyValue<SavingWrapper>(GetSavingWrapper);
        }

        void Update()
        {
            if (playGame)
            {
                timer.UpdateTimer(Time.deltaTime);
                if (OnSliderChanged != null)
                {
                    OnSliderChanged();
                }
                if (timer.GetCurrentTime() > timer.GetLevelTime())
                {
                    LoseLife();
                }
            }
        }

        public int GetScore()
        {
            return score.GetScore();
        }

        public int GetHighScore()
        {
            return score.GetHighScore();
        }

        public int GetLives()
        {
            return lives.GetLivesRemaining();
        }

        public bool GetPlayGame()
        {
            return playGame;
        }

        public void SetHighScore(int highScore)
        {
            score.SetHighScore(highScore);
        }

        public void SetPlayGame(bool playGame)
        {
            this.playGame = playGame;
        }

        public void IncreaseScore(int pointsValue)
        {
            int newScore = score.GetScore() + pointsValue;
            if (newScore > scoreForExtraLife && score.GetScore() < scoreForExtraLife)
            {
                GainLife();
            }
            score.SetScore(newScore);
            if (OnScoreChanged != null)
            {
                OnScoreChanged();
            }
        }

        public void LevelComplete()
        {
            SetPlayGame(false);
            StartCoroutine(ConvertAirToScore());
            levelLoader.value.LoadNextLevel();
        }

        public void LoseLife()
        {
            SetPlayGame(false);
            lives.SetLivesRemaining(GetLives() - 1);
            if (OnLivesChanged != null)
            {
                OnLivesChanged();
            }
            if (GetLives() <= 0)
            {
                levelLoader.value.LoadGameOver();
            }
            else
            {
                levelLoader.value.RestartLevel();
            }
        }

        public void GainLife()
        {
            lives.SetLivesRemaining(GetLives() + 1);
            if (OnLivesChanged != null)
            {
                OnLivesChanged();
            }
        }

        public IEnumerator ConvertAirToScore()
        {
            playGame = false;
            float timeRemaining = timer.GetLevelTime() - timer.GetCurrentTime();
            for (float i = 0; i < timeRemaining; i++)
            {
                IncreaseScore(scorePerAirUnit);
                timer.UpdateTimer(1.0f);
                if (OnSliderChanged != null)
                {
                    OnSliderChanged();
                }
                yield return new WaitForSeconds(delay);
            }
        }

        public void ResetGame()
        {
            lives.ResetLives();
            score.SetScore(0);
            playGame = true;
            if (OnLivesChanged != null)
            {
                OnLivesChanged();
            }
            if (OnScoreChanged != null)
            {
                OnScoreChanged();
            }
            savingWrapper.value.LoadGame();
        }

        public float GetNormalisedTime()
        {
            return timer.GetNormalisedTime();
        }

        public void LoadMainMenu()
        {
            levelLoader.value.LoadMainMenu();
        }

        public void NewGame()
        {
            ResetGame();
            levelLoader.value.NewGame();
        }

        public void QuitGame()
        {
            levelLoader.value.QuitGame();
        }

        public void SaveGame()
        {
            savingWrapper.value.SaveGame();
        }

        private LevelLoader GetLevelLoader()
        {
            return FindObjectOfType<LevelLoader>();
        }

        private SavingWrapper GetSavingWrapper()
        {
            return GetComponent<SavingWrapper>();
        }
    }
}