﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Auth.Signature;
public class LoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
