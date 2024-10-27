﻿using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class ZipCode
{
    public string PostalCode { get; set; }
    public string? Name { get; set; }
    public ulong CountryId { get; set; }
    public Country Country { get; set; }
}