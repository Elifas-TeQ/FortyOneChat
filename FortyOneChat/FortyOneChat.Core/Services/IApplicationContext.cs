﻿using FortyOneChat.Core.Models;

namespace FortyOneChat.Core.Services
{
    public interface IApplicationContext
    {
        User CurrentUser { get; set; }
		string ServiceUri { get; }
    }
}