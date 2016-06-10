﻿using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Abstract
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> Entities{ get; set; }
    }
}
