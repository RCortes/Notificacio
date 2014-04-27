using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_PushBBVA
{
    public class NotificationType
    {
        private int _idNotificationType;

        public int idNotificationType
        {
            get { return _idNotificationType; }
            set { _idNotificationType = value; }
        }

        private string _description;

        public string description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _text;

        public string text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
