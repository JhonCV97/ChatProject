using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"La propiedad {name} : Con el valor  '({key})' no se encontró")
        {
        }
        public NotFoundException(string name)
            : base(name)
        {
        }
    }
}
