using System.Collections.Generic;

namespace Dialogue
{
    public class NullDialogue : IDialogue
    {
        public bool CanContinue { get => false; }
        public bool HasChoices { get => false; }

        private readonly string EmptyText = "К сожалению, данный мир был уничтожен, и вы не сможете его посетить.";
        private readonly List<string> Metadata = new List<string>()
        {
            "add:choices,_,MainChoicesList,0,0",
            "add:window,_,MainWindow,0,-160"
        };

        public void Reset() {}
        public void Continue() {}
        public string GetText() => EmptyText;
        public List<string> GetMetadata()
        {
            return Metadata;
        }
    }
}
