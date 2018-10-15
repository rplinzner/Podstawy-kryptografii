using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESX_Model
{
    public class InputHandler
    {
        private readonly string _plainText;
        private readonly byte[] _plainTextInBytes;
        private readonly Encoding _encoding;

        public InputHandler(string plainText, Encoding encoding)
        {
            _plainText = plainText;
            _encoding = encoding;
            _plainTextInBytes = encoding.GetBytes(plainText);
        }

        public byte[] PlainTextInBytes => _plainTextInBytes;
    }
}
