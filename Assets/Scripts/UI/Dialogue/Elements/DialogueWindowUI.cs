namespace UI.Dialogue.Elements
{
    public class DialogueWindowUI : CompositeDialogueUIElement
    {
        public override void Accept(DialogueSaverVisitor visitor)
        {
            visitor.VisitMainWindow(this);
        }

        protected override void AcceptOwnElement(DialogueSaverVisitor visitor) { }
    }
}
