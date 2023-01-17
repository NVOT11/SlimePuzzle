using UnityEngine;
using Stage;
using UnityEngine.U2D.Animation;

namespace Common
{

    [CreateAssetMenu(fileName = nameof(GameSetting), menuName = nameof(GameSetting))]
    public class GameSetting : ScriptableObject
    {
        /// <summary>
        /// メッセージテキストのデータ
        /// </summary>
        public TextAsset DialogText => _dialogTexts;
        [SerializeField] private TextAsset _dialogTexts;

        /// <summary>
        /// Stage情報
        /// </summary>
        public TextAsset StageInfo => _stageInfo;
        [SerializeField] private TextAsset _stageInfo;

        /// <summary>
        /// Slime
        /// </summary>
        public SlimeObject ProtoType => _prototype;
        [SerializeField] private SlimeObject _prototype;

        /// <summary>
        /// MapのPrefab
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
        /// HUD用のアイコン
        /// </summary>
        public Sprite[] Icons => _icons;
        [SerializeField] private Sprite[] _icons;
    }
}