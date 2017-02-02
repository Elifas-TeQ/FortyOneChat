using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FortyOneChatServices.Models
{
    public class Message
    {
        public int Id { get; set; }
        public User Author { get; set; }
        public string Text { get; set; }
        public DateTime DateCreated { get; set; }
    }
}