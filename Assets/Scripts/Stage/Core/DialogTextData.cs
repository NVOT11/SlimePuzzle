using System.Linq;
using Common.StaticData;

namespace Stage
{
    public class DialogTextData
    {
        public DialogTextData(DialogText[] dialogs) 
        {
            _dialogs = dialogs;
        }

        private DialogText[] _dialogs;

        public string GetText(string id)
        {
            return _dialogs.Where(_ => _.ID == id).FirstOrDefault()?.Text ?? "";
        }
    }
}