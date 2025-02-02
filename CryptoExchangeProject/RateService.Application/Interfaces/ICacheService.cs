﻿namespace RateService.Application.Interfaces;

public interface ICacheService
{
    Task<string> GetAsync(string key);
    Task SetAsync(string key, string value, TimeSpan expiration);

}