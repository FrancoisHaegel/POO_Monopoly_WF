using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.exception
{
    class ImpossibleMortgageException : Exception
    {
        public ImpossibleMortgageException()
    : base()
        {
        }

        public ImpossibleMortgageException(String message)
    : base(message)
        {
        }

        public ImpossibleMortgageException(String message, Exception innerException)
    : base(message, innerException)
        {
        }
    }
}