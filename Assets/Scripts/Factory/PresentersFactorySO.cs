using UnityEngine;

using UI.Dialogue;

namespace Factory
{
    [CreateAssetMenu(fileName="PresentersFactorySO")]
    public class PresentersFactorySO : DefaultFactorySO<IDialoguePresenterBehaviour> { }
}
