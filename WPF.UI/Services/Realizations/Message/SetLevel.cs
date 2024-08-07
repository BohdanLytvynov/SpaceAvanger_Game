﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.UI.Services.Interfaces.Message;

namespace WPF.UI.Services.Realizations.Message
{
    internal class SetLevel : IGameMessage<string>
    {
        public string Args { get; }

        public SetLevel(string name)
        {
            Args = name;
        }
    }
}
