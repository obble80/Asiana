using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;

namespace Asiana.UI.Filters
{
    public class MailCSSStream : MemoryStream
    {
        private StringBuilder outputString = new StringBuilder();
        private Stream outputStream = null;

        public MailCSSStream(Stream outputStream)
        {
            this.outputStream = outputStream;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            outputString.Append(Encoding.UTF8.GetString(buffer));
        }

        public override void Close()
        {
            //Call the minifier here, your data is in outputString
            string result = outputString.ToString().Replace(Environment.NewLine, string.Empty);

            byte[] rawResult = Encoding.UTF8.GetBytes(result);
            outputStream.Write(rawResult, 0, rawResult.Length);

            base.Close();
            outputStream.Close();
        }

    }
}
