using System.Text;

namespace UI.Dialogue.Elements
{
    public class DialogueSaverVisitor
    {
        private StringBuilder _dialogueStateString = new StringBuilder();

        private readonly string _addAnimationWrapperString = "add:animation,AnimationsContainer,AnimationWrapper";
        private readonly string _setAnimationString = "animation:";
        private readonly string _addChoicesListString = "add:choices,_,MainChoicesList,0,0";
        private readonly string _addMainWindowString = "add:window,_,MainWindow,0,-160";
        private readonly string _addContainerString = "add:container,_,AnimationsContainer,0,0";
        private readonly string _setMainTextString = "text:MainText";
        private readonly string _addChoiceString = "add:choice,MainChoicesList,";
        private readonly string _setChoiceTextString = "text:";
        private readonly string _defaultChoiceIdPrefix = "_choice";
        private readonly string _textIdString = "Text";

        private readonly char _argumentsDelim = ',';
        private readonly char _commandsDelim = ';';
        private readonly char _idDelim = '.';


        public string GetSavedDialogueState()
        {
            return _dialogueStateString.ToString();
        }

        public void VisitAnimationWrapper(AnimationWrapper animationWrapper)
        {
            _dialogueStateString.Append(_addAnimationWrapperString);
            _dialogueStateString.Append(_commandsDelim);

            _dialogueStateString.Append(_setAnimationString);
            _dialogueStateString.Append(animationWrapper.ID);
            _dialogueStateString.Append(_argumentsDelim);
            _dialogueStateString.Append(animationWrapper.CurrentAnimation);
            _dialogueStateString.Append(_commandsDelim);
        }

        public void VisitChoicesList(ChoicesListUI choicesList)
        {
            _dialogueStateString.Append(_addChoicesListString);
            _dialogueStateString.Append(_commandsDelim);

            for (int i = 0; i < choicesList.ChoicesCount; ++i)
            {
                string currentChoice = _defaultChoiceIdPrefix + i;
                _dialogueStateString.Append(_addChoiceString);
                _dialogueStateString.Append(currentChoice);
                _dialogueStateString.Append(_commandsDelim);

                _dialogueStateString.Append(_setChoiceTextString);
                _dialogueStateString.Append(currentChoice);
                _dialogueStateString.Append(_idDelim);
                _dialogueStateString.Append(_textIdString);
                _dialogueStateString.Append(_argumentsDelim);
                _dialogueStateString.Append(i);
                _dialogueStateString.Append(_commandsDelim);
            }
        }

        public void VisitMainWindow(DialogueWindowUI dialogueWindow)
        {
            if (dialogueWindow.ID == "MainWindow")
            {
                _dialogueStateString.Append(_addMainWindowString);
                _dialogueStateString.Append(_commandsDelim);

                _dialogueStateString.Append(_setMainTextString);
                _dialogueStateString.Append(_commandsDelim);
                return;
            }

            _dialogueStateString.Append(_addContainerString);
            _dialogueStateString.Append(_commandsDelim);

            dialogueWindow.AcceptChildren(this);
        }
    }
}
