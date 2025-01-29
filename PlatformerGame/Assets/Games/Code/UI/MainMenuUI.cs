using System;
using UnityEngine;
using UnityEngine.UI;

namespace BP.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private Button _optionsButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _continueButton.onClick.AddListener(ContinueGame);
            _startGameButton.onClick.AddListener(NewGame);
            _loadGameButton.onClick.AddListener(LoadGame);
            _optionsButton.onClick.AddListener(OptionsMenu);
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(ContinueGame);
            _startGameButton.onClick.RemoveListener(NewGame);
            _loadGameButton.onClick.RemoveListener(LoadGame);
            _optionsButton.onClick.RemoveListener(OptionsMenu);
            _exitButton.onClick.RemoveListener(ExitGame);
        }

        public void ContinueGame()
        {
            
        }

        public void NewGame()
        {
            
        }

        public void LoadGame()
        {
            
        }

        public void OptionsMenu()
        {
            
        }

        public void ExitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            
            Application.Quit();
        }
    }
}
