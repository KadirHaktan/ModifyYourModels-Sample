using System;
using System.Collections.Generic;
using System.Text;
using Core.Interfaces.Entities;

namespace Core.Interfaces.Repository
{
    public interface IIEntityRepository<T>:IRepository<T> where T:class,IEntity
    {
    }
}
