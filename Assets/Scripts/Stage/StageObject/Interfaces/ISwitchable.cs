
namespace Stage
{
    /// <summary>
    /// スイッチによって動作するオブジェクトに実装する
    /// </summary>
    public interface ISwitchable
    {
        public void OnSwitchChanged(bool value);
    }
}
