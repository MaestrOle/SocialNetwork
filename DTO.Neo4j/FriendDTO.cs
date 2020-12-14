using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Neo4j
{
    public class FriendDTO
    {
        [JsonProperty(PropertyName = "length")]
        public string lenght { get; set; }
    }
}