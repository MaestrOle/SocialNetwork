using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Neo4j
{
    public class UserDTO
    {
        [JsonProperty(PropertyName = "UserID")]
        public int User_Id { get; set; }
        [JsonProperty(PropertyName = "UserEmail")]
        public string User_Email { get; set; }

        [JsonProperty(PropertyName = "FisrtName")]
        public string User_Name { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string User_LastName { get; set; }
    }
}