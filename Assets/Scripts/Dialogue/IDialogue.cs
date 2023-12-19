using System.Collections.Generic;

namespace Dialogue
{
    public interface IDialogue
    {
        public bool CanContinue { get; }
        public bool HasChoices { get; }

        public void Reset();
        public void Continue();
        public string GetText();
        public string GetDialogueState();
        public string GetChoiceText(int choiceId);
        public void MakeChoice(int choiceId);
        public List<string> GetMetadata();
    }
}
