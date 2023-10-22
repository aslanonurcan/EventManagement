﻿using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
    }
}
