using UnityEngine.U2D.Animation;

namespace Stage
{
    public class LibrariyList
    {
        public LibrariyList(SpriteLibraryAsset[] library)
        {
            _library = library;
        }

        private SpriteLibraryAsset[] _library;

        public SpriteLibraryAsset GetSpriteLibrary(SlimeMode mode)
        {
            return mode switch
            {
                SlimeMode.Green => _library[0],
                SlimeMode.Red => _library[1],
                SlimeMode.Blue => _library[2],
                _ => _library[0]
            };
        }
    }

}