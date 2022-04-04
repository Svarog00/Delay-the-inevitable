using UnityEngine.UI;
using UnityEngine;
using Assets.Code.Global;
using System;

namespace Assets.Code.UI
{
    public class TimerScript : MonoBehaviour
    {
        private Text _textField;
        private GameStateManager _gameStateManager;

        private void Start()
        {
            _textField = GetComponent<Text>();
            _gameStateManager = FindObjectOfType<GameStateManager>();
            _gameStateManager.OnTimeChangedEvent += _gameStateManager_OnTimeChangedEvent;
        }

        private void _gameStateManager_OnTimeChangedEvent(object sender, GameStateManager.OnTimeChangedEventArg e)
        {
            _textField.text = $"{Math.Round(e.Time, 2)}";
        }
    }
}
