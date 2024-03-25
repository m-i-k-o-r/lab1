using lab1;
using lab1.MessageTypeSpace;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace lab1.ViewModel
{
    public class LogMessageVM : BindableBase
    {
        #region Fields
        private String _text;

        private Int32 _count;

        private Int32 _type;

        private Int32 _stype;

        private String _starttime;
        
        private String _endtime;

        private List<Message> _messages;
        #endregion

        #region Commands
        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand GetPartByTypeCommand { get; private set; }
        public DelegateCommand GetPartByTimeCommand { get; private set; }
        public DelegateCommand ClearCommand { get; private set; }
        public DelegateCommand SaveCommand { get; private set; }
        public ObservableCollection<String> Items { get; private set; } = new ObservableCollection<String>();
        public ObservableCollection<String> PartItems { get; private set; } = new ObservableCollection<String>();
        #endregion

        #region Constructors
        public LogMessageVM()
        {
            AddCommand = new DelegateCommand(AddItem);
            GetPartByTypeCommand = new DelegateCommand(GetPartByType);
            GetPartByTimeCommand = new DelegateCommand(GetPartByTime);
            ClearCommand = new DelegateCommand(ClearAll);
            SaveCommand = new DelegateCommand(SaveAll);

            Count = Items.Count;

            _messages = new List<Message>();

            _starttime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            _endtime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }
        #endregion

        #region Private Realisation
        private void AddItem()
        {
            Message message = new Message((MessageType)_type, DateTime.Now, _text);
            _messages.Add(message);

            Items.Add(message.GetMessageType + " : " + message.GetDateTime + " : " + message.Text);
            
            Count = Items.Count;
        }

        private void GetPartByType()
        {
            PartItems.Clear();
            
			foreach(Message message in _messages)
            {
                if ((int)message.GetMessageType == _stype)
                {
                    PartItems.Add(message.GetMessageType + " : " + message.GetDateTime + " : " + message.Text);
                }
            }
        }

        private void GetPartByTime()
        {
            PartItems.Clear();

            foreach (Message message in _messages)
            {
                if (DateTime.Parse(_endtime) > message.GetDateTime && message.GetDateTime > DateTime.Parse(_starttime))
                {
                    PartItems.Add(message.GetMessageType + " : " + message.GetDateTime + " : " + message.Text);
                }
            }
        }

        private void ClearAll()
        {
            PartItems.Clear();
            Items.Clear();
            _messages.Clear();
        }

        private void SaveAll()
        {
            List<string> text = new List<string>();
            foreach (Message message in _messages) 
            {
                text.Add(message.GetMessageType + " : " + message.GetDateTime + " : " + message.Text);
            }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt"; 
            saveFileDialog.FileName = "file.txt"; 
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllLines(filePath, text);
            }
        }
        #endregion

        #region Properties
        public string AddText
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public Int32 Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        public Int32 AddType
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public Int32 SortType
        {
            get => _stype;
            set => SetProperty(ref _stype, value);
        }

        public String StartTime
        {
            get => _starttime;
            set => SetProperty(ref _starttime, value);
        }

        public String EndTime
        {
            get => _endtime;
            set => SetProperty(ref _endtime, value);
        }
        #endregion
    }
}
