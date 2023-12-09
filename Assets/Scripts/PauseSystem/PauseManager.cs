using UnityEngine;

namespace PauseSystem
{
    public class PauseManager : MonoBehaviour
    {
        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Unpause()
        {
            Time.timeScale = 1;
        }
    }
}
