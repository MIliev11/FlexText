using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;

namespace FlexText
{
    public class MainPageViewModel : INotifyPropertyChanged
    {

        private const char PARSE_HIDDEN_WORD_CHAR_FROM = '[';
        private const char PARSE_HIDDEN_WORD_CHAR_TO = ']';
        private const char PARSE_HIDDEN_WORD_CHAR = '*';

        public MainPageViewModel()
        {
            OnButtonClick = new Command(HandleInputWord);
            Text = "sdfds fsdfsdfsdfd dsfdf [***********] sdfsdfsd ad [**dfdfd*] sdf sfd sdf sdfdff [***ddd] sfdgfgsffs sdfsdfsdr sscf [***]";
        }

        #region -- Public properties --

        private string _text;
        public string Text
        {
            get => _text;
            private set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged("Text");
                }
            }
        }

        public ICommand OnButtonClick { get; private set; }

        #endregion

        #region -- Private helpers --

        private void HandleInputWord(object obj)
        {
            if (!(obj is string))
                return;

            string word = obj as string;

            int start = Text.IndexOf(PARSE_HIDDEN_WORD_CHAR_FROM);
            if (start >= Text.Length || start < 0)
                return;

            int end = Text.IndexOf(PARSE_HIDDEN_WORD_CHAR_TO);
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(Text.Substring(0, start));
            stringBuilder.Append(word);
            stringBuilder.Append(Text.Substring(end + 1, Text.Length - end - 1));

            Text = stringBuilder.ToString();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region -- INotifyPropertyChanged implementation --

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
