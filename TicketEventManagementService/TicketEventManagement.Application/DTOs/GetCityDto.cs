﻿namespace TicketEventManagement.Application.DTOs;

public class GetCityDto
{
    public Guid CityId { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }
}
