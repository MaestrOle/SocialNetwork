﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace DAL.Cassandra.Interfaces
{
    public interface IPosts
    {
         void CreatePost(ISession session);
         void UpdatePost(ISession session);

    }
}
