using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cassandra;

namespace DTO.Cassandra
{
    public class CommentsDTO
    {
        public TimeUuid CommentId { get; set; }
        public string Comment { get; set; }
        public Guid Commentator { get; set; }
    }
}
