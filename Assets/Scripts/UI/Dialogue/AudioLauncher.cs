using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Dialogue
{
    public class AudioLauncher : IDialoguePresenterBehaviour
    {
        private readonly string SoundType = "sound";
        private readonly string MusicType = "music";
        private readonly int ClipIndex = 0;
        private readonly int TypeIndex = 1;


        public void DoBehaviour(DialoguePresenter presenter, params string[] parameters)
        {
            if (parameters.Length <= TypeIndex)
            {
                Debug.LogError($"parameters doesn't have enough parameters: its length is {parameters.Length}");
                return;
            }

            string clipName = parameters[ClipIndex];
            string clipType = parameters[TypeIndex].ToLower();
            if (clipType == SoundType)
            {
                presenter.PlaySound(clipName);
            }
            else if (clipType == MusicType)
            {
                presenter.PlayMusic(clipName);
            }
            else
            {
                Debug.LogError($"there is not clip type {clipType}");
            }
        }
    }
}
