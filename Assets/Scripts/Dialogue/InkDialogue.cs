using System.Collections.Generic;
using Ink.Runtime;

namespace Dialogue
{
    public class InkDialogue : IDialogue
    {
        public bool CanContinue { get => _story.canContinue; }
        public bool HasChoices { get => throw new System.NotImplementedException(); }

        private Story _story;


        public InkDialogue(string text)
        {
            _story = new Story(text);
        }

        public void Reset()
        {
            _story.ResetState();
        }

        public void Continue()
        {
            _story.Continue();
        }

        public string GetText()
        {
            return _story.currentText;
        }

        public List<string> GetMetadata()
        {
            return _story.currentTags;
        }
    }
}
