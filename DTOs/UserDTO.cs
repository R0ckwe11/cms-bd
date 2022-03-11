using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cms_bd.Models;

namespace cms_bd.DTOs;

public class UserDTO
{
    public long ID { get; set; }
    public string UserName { get; set; }

    public UserDTO(User user)
    {
        ID = user.Id;
        UserName = user.UserName;
    }
}
