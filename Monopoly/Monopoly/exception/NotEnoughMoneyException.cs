using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.exception
{
    class NotEnoughMoneyException : Exception
    {
        public NotEnoughMoneyException()
    : base()
        {
        }

        public NotEnoughMoneyException(String message)
    : base(message)
        {
        }

        public NotEnoughMoneyException(String message, Exception innerException)
    : base(message, innerException)
        {
        }
    }
}
