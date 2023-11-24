using System.Collections.Generic;

namespace Dialogue
{
    public class NullDialogue : IDialogue
    {
        public bool CanContinue { get => _index < _maxIndex; }
        public bool HasChoices { get => false; }

        private readonly string[] Texts = new string[2]
        {
            "К сожалению, данный мир был уничтожен.",
            "Вы не сможете его посетить."
        };
        private readonly List<List<string>> Metadatas = new List<List<string>>()
        {
            new List<string>()
            {
                "add:choices,_,MainChoicesList,0,0",
                "add:window,_,MainWindow,0,-160",
                "text:MainText"
            },
            new List<string>()
            {
                "text:MainText"
            }
        };
        private readonly int _maxIndex = 1;
        private int _index = 0;

        public void Reset() => _index = 0;
        public void Continue() => _index++;
        public string GetText() => Texts[_index];
        public List<string> GetMetadata() => Metadatas[_index];
    }
}
