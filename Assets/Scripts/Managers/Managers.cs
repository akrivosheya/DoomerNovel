using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Managers : MonoBehaviour
    {
        private List<IManager> _managers = new List<IManager>();

        void Start()
        {
            StartCoroutine(InitializeManagers());
        }

        private IEnumerator InitializeManagers()
        {
            yield return null;

            foreach (IManager manager in _managers)
            {
                manager.Initialize();
            }

            yield return null;

            bool managersAreReady = false;
            while (!managersAreReady)
            {
                managersAreReady = true;
                foreach (IManager manager in _managers)
                {
                    managersAreReady &= manager.IsReady;
                }
                
                yield return null;
            }
        }
    }
}
