using UnityEngine;

namespace UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogueBox;

        void Awake()
        {
            _dialogueBox.SetActive(false);
        }

        public void Activate()
        {
            _dialogueBox.SetActive(true);
        }
    }
}
