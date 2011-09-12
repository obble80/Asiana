using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asiana.Profile.Services.Reviews
{
    public class Comment
    {
        public string UserID { get; set; }
        public string UserNickname { get; set; }
        public string ShortComment { get; set; }
        public string FullComment { get; set; }
    }
}
