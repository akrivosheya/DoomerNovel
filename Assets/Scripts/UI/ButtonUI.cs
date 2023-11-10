using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonUI : LanguageDependentUI
    {
        [SerializeField] private GameObject _button;
        [SerializeField] private Text _buttonText;

        [SerializeField] private string _buttonType;


        public override void SetActive(bool isActive)
        {
            _button.SetActive(isActive);

            UpdateLanguageDependentContext();
        }

        protected override void UpdateLanguageDependentContext()
        {
            if (_button.activeSelf && HasLanguageMismatch)
            {
                _buttonText.text = GetText(_buttonType);
            }
        }
    }
}
