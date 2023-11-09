using System.Collections.Generic;

namespace Dialogue
{
    public interface IDialogue
    {
        public bool CanContinue { get; }

        public void Reset();
        public void Continue();
        public string GetText();
        public List<string> GetMetadata();
    }
}
