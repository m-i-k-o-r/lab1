using lab1.MessageTypeSpace;

namespace lab1
{
    public class Message
    {
        #region Fields
        private MessageType _messageType;
        private DateTime _date;
        private String _text;
        #endregion

        #region Constructors
        public Message(MessageType messageType, DateTime date, String text)
        {
            _messageType = messageType;
            _date = date;
            _text = text;
        }
        #endregion

        #region Properties
        public MessageType GetMessageType
        {
            get => _messageType;
        }

        public DateTime GetDateTime
        {
            get => _date;
        }

        public String Text { get => _text; }        
        #endregion
    }
}
