using UnityEngine;
using Stage;
using UnityEngine.U2D.Animation;

namespace Common
{

    [CreateAssetMenu(fileName = nameof(GameSetting), menuName = nameof(GameSetting))]
    public class GameSetting : ScriptableObject
    {
        /// <summary>
        /// ���b�Z�[�W�e�L�X�g�̃f�[�^
        /// </summary>
        public TextAsset DialogText => _dialogTexts;
        [SerializeField] private TextAsset _dialogTexts;

        /// <summary>
        /// Stage���
        /// </summary>
        public TextAsset StageInfo => _stageInfo;
        [SerializeField] private TextAsset _stageInfo;

        /// <summary>
        /// Slime
        /// </summary>
        public SlimeObject ProtoType => _prototype;
        [SerializeField] private SlimeObject _prototype;

        /// <summary>
        /// Map��Prefab
        /// </summary>
        public MapObject[] MapObjects => _mapObjects;
        [SerializeField] private MapObject[] _mapObjects;

        public EndMapObject EndMapObject => _endMapObject;
        [SerializeField] private EndMapObject _endMapObject;

        /// <summary>
        /// Sprite
        /// </summary>
        public SpriteLibraryAsset[] Libiraries => _libraries;
        [SerializeField] private SpriteLibraryAsset[] _libraries;

        /// <summary>
        /// HUD�p�̃A�C�R��
        /// </summary>
        public Sprite[] Icons => _icons;
        [SerializeField] private Sprite[] _icons;
    }
}