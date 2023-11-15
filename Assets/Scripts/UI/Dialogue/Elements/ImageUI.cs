using UnityEngine;
using UnityEngine.UI;

using Factory;

namespace UI.Dialogue.Elements
{
    public class ImageUI : DialogueUIElement
    {
        public Sprite CurrentSprite { get => _image.sprite; }

        [SerializeField] private Image _image;

        [SerializeField] private string _spritesFolder = "Sprites";
        [SerializeField] private int _minParameters = 4;
        [SerializeField] private int _spriteIndex = 3;


        public override void Initialize(params string[] initParameters)
        {
            if (initParameters.Length < _minParameters)
            {
                throw new InitializationException($"image need minimum {_minParameters}, but got {initParameters.Length}");
            }

            string spriteFile = initParameters[_spriteIndex];
            string spritePath = System.IO.Path.Join(_spritesFolder, spriteFile);
            Sprite sprite = Resources.Load<Sprite>(spritePath);
            if (sprite is null)
            {
                throw new InitializationException($"can't load sprite {spritePath}");
            }
            _image.sprite = sprite;

            base.Initialize(initParameters);
        }

        public override void SetActive(bool isActive)
        {
            _image.gameObject.SetActive(isActive);
        }

        public override Rect GetRect()
        {
            return _image.rectTransform.rect;
        }
    }
}
