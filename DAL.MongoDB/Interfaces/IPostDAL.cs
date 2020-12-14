using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MongoDB.Interfaces
{
    public interface IPostDal
    {
        void ReadAll();
        Task CreatePostAsync(string e);
        Task PostOverviewAsync(string f, string l, string e);

    }
}

