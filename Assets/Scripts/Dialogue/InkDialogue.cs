using System.Collections.Generic;
using Ink.Runtime;

using UnityEngine;

namespace Dialogue
{
    public class InkDialogue : IDialogue
    {
        public bool CanContinue { get => _story.canContinue; }
        public bool HasChoices { get => _story.currentChoices.Count > 0; }

        private Story _story;
        private List<string> _loadedMetadata = new List<string>();


        public InkDialogue(string text)
        {
            _story = new Story(text);
        }

        public string GetDialogueState()
        {
            return (_story is null) ? string.Empty : _story.state.ToJson();
        }

        public void Reset()
        {
            _story.ResetState();
        }

        public void SetState(string stateJson)
        {
            _story.state.LoadJson(stateJson);
        }

        public void SetMetadata(List<string> metadata)
        {
            _loadedMetadata = metadata;
        }

        public void Continue()
        {
            _story.Continue();
        }

        public string GetText()
        {
            return _story.currentText;
        }

        public string GetChoiceText(int choiceId)
        {
            if (!HasChoices || _story.currentChoices.Count <= choiceId || choiceId < 0)
            {
                return string.Empty;
            }
            
            return _story.currentChoices[choiceId].text;
        }

        public void MakeChoice(int choiceId)
        {
            if (!HasChoices)
            {
                Debug.LogError($"can't make choice now");
                return;
            }

            if (choiceId < 0 || choiceId >= _story.currentChoices.Count)
            {
                Debug.LogError($"choice id must be integer more or equal 0 and less than {_story.currentChoices.Count}");
            }

            _story.ChooseChoiceIndex(choiceId);
            _story.Continue();
            _story.Continue();
        }

        public List<string> GetMetadata()
        {
            if (_loadedMetadata.Count > 0)
            {
                List<string> returnMetadata = _loadedMetadata;
                _loadedMetadata = new List<string>();
                return returnMetadata;
            }

            if (!HasChoices)
            {
                return _story.currentTags;
            }

            List<string> tags = new List<string>();
            foreach (Choice choice in _story.currentChoices)
            {
                if (!(choice.tags is null))
                {
                    tags.AddRange(choice.tags);
                }
            }
            return tags;
        }
    }
}
