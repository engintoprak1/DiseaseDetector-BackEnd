using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Policlinic : IEntity
    {
        public int Id { get; set; }

        public string PoliclinicName { get; set; }
    }
}
