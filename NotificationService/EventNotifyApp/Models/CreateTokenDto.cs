﻿namespace EventNotifyApp.Models;

public class CreateTokenDto
{
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
}
