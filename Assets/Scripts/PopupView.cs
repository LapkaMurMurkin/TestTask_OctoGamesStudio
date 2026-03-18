using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace TestTask_OctoGames
{
    public class PopupView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Text _titleText;
        [SerializeField] private Text _bodyText;

        [Header("Buttons")]
        [SerializeField] private Button _buttonLeft;
        [SerializeField] private Button _buttonMiddle;
        [SerializeField] private Button _buttonRight;

        private Button[] _buttons;
        private Text[] _buttonsText;

        private void Awake()
        {
            if (_titleText == null) throw new MissingReferenceException(nameof(_titleText));
            if (_bodyText == null) throw new MissingReferenceException(nameof(_bodyText));

            if (_buttonLeft == null) throw new MissingReferenceException(nameof(_buttonLeft));
            if (_buttonMiddle == null) throw new MissingReferenceException(nameof(_buttonMiddle));
            if (_buttonRight == null) throw new MissingReferenceException(nameof(_buttonRight));

            _buttons = new[] { _buttonLeft, _buttonMiddle, _buttonRight };
            _buttonsText = new Text[_buttons.Length];

            for (int i = 0; i < _buttons.Length; i++)
            {

                Text textComponent = _buttons[i].GetComponentInChildren<Text>(true);
                if (textComponent is null)
                    throw new MissingComponentException(
                        $"{nameof(Button)} requires {nameof(Text)} component on {_buttons[i].name}"
                    );

                _buttonsText[i] = textComponent;
                _buttons[i].gameObject.SetActive(false);
            }
        }

        public void Show(string title, string body, List<PopupButtonData> buttons)
        {
            gameObject.SetActive(true);

            _titleText.text = title;
            _bodyText.text = body;

            SetupButtons(buttons);
        }

        private void SetupButtons(List<PopupButtonData> buttonsData)
        {
            if (buttonsData == null || buttonsData.Count == 0)
            {
                Debug.LogWarning("Popup has no buttons");
                return;
            }

            if (buttonsData.Count > 3)
            {
                throw new ArgumentException("Popup supports max 3 buttons");
            }

            for (int i = 0; i < _buttons.Length; i++)
            {
                Button button = _buttons[i];
                button.gameObject.SetActive(false);
                button.onClick.RemoveAllListeners();
            }

            for (int i = 0; i < buttonsData.Count; i++)
            {
                PopupButtonData data = buttonsData[i];
                Button button = _buttons[i];
                Text buttonText = _buttonsText[i];

                buttonText.text = data.Text;

                button.onClick.AddListener(() =>
                {
                    data.Callback?.Invoke();
                    Hide();
                });

                button.gameObject.SetActive(true);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}