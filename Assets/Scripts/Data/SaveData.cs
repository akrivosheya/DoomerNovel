namespace Data
{
    public struct SaveData
    {
        public string SaveImage { get; private set; }
        public string SaveInformation { get; private set; }
        public string DialogueState { get; private set; }
        public string SceneState { get; private set; }

        public SaveData(string saveImage, string saveInformation, string dialogueState, string sceneState)
        {
            SaveImage = saveImage;
            SaveInformation = saveInformation;
            DialogueState = dialogueState;
            SceneState = sceneState;
        }
    }
}
