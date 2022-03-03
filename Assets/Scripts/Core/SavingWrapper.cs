using MM.Saving;
using UnityEngine;

namespace MM.Core
{
    public class SavingWrapper : MonoBehaviour
    {
        const string saveFile = "save";

        LazyValue<SavingSystem> savingSystem = null;

        void Awake()
        {
            savingSystem = new LazyValue<SavingSystem>(GetSavingSystem);
        }

        public void SaveGame()
        {
            savingSystem.value.Save(saveFile);
        }

        public void LoadGame()
        {
            savingSystem.value.Load(saveFile);
        }

        private SavingSystem GetSavingSystem()
        {
            return FindObjectOfType<SavingSystem>();
        }
    }
}