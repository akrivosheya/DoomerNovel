using System.Collections.Generic;

namespace UI.Dialogue
{
    public class UIElementAdder : IDialoguePresenterBehaviour
    {
        private int _elementTypeIndex = 0;
        private int _containerIdIndex = 1;
        private int _initParametersBegin = 2;


        public void DoBehaviour(DialoguePresenter presenter, params string[] parameters)
        {
            string elementType = parameters[_elementTypeIndex];
            string containerId = parameters[_containerIdIndex];
            List<string> parametersList = new List<string>(parameters);
            parametersList.RemoveRange(0, _initParametersBegin);
            parameters = parametersList.ToArray();
            presenter.AddElement(elementType, containerId, parameters);
        }
    }
}
