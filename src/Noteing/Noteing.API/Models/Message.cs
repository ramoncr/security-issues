﻿using MimeKit;

namespace Noteing.API.Models
{
    public class Message
    {
        public MailboxAddress To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }


    }
}
