using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace TestTask_OctoGames
{
    public class CharactersView : MonoBehaviour
    {
        [SerializeField] private List<Character> _characters;
        [SerializeField] private Text _text;
        [SerializeField] private float _updateInterval = 1f;

        private float _timer;

        private void Awake()
        {
            if (_characters is null)
                throw new System.NullReferenceException(
                    $"{nameof(_characters)} is not assigned in {nameof(CharactersView)} on {gameObject.name}"
                );

            if (_text is null)
                throw new MissingComponentException(
                    $"{nameof(CharactersView)} requires {nameof(Text)} component on {gameObject.name}"
                );
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _updateInterval)
            {
                _timer -= _updateInterval;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            if (_characters.Count == 0)
            {
                _text.text = "No characters";
                return;
            }

            float totalValue = 0f;
            int validCharacters = 0;

            foreach (Character character in _characters)
            {
                if (character == null)
                    continue;

                totalValue += character.Value;
                validCharacters++;
            }

            float averageValue = validCharacters > 0
                ? totalValue / validCharacters
                : 0f;

            string text = $"Characters: {validCharacters} | Avg value: {averageValue:F2}";
            _text.text = text;

            Debug.Log(text);
        }
    }
}
