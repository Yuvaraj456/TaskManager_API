﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager_Core.Domain.Entities
{
    public class TokenModel
    {
        public string? Token { get; set; }

        public string? RefreshToken { get; set; }
    }
}