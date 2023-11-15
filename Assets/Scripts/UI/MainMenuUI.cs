using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class MainMenuUI : MenuElementsContainerUI
    {
        [SerializeField] private string _exitMessage = "exitGame";


        public void OnExitGame()
        {
            ShowConfirmWindow(_exitMessage, OnConfirmExit, OnDenyExit);
        }

        private void OnConfirmExit()
        {
            Application.Quit();
        }

        private void OnDenyExit()
        {
            DestroyWindow();
        }
    }
}
