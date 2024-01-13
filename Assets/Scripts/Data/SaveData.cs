namespace Data
{
    public struct SaveData
    {
        public static SaveData EmptyData { get; } = new SaveData() { IsEmpty=true };
        public string SaveImage { get; }
        public string SaveInformation { get; }
        public string DialogueState { get; }
        public string SceneState { get; }
        public bool IsEmpty { get; private set; }


        public SaveData(string saveImage, string saveInformation, string dialogueState, string sceneState)
        {
            SaveImage = saveImage;
            SaveInformation = saveInformation;
            DialogueState = dialogueState;
            SceneState = sceneState;
            IsEmpty = false;
        }

        public SaveData(SaveDataDTO saveDataDTO) :
        this(
            saveDataDTO.SaveImage,
            saveDataDTO.SaveInformation,
            saveDataDTO.DialogueState,
            saveDataDTO.SceneState
        ) { }
    }
}
