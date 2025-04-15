using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.DTOs.DeviceDtos
{
    public record DeviceForCreationDto(
    [MaxLength(8)] string? DomainIDIfExists,
    [MaxLength(50)] string? Owner,
    [MaxLength(11)] string? PhoneNmber,
    [MaxLength(50)] string Type,
    [MaxLength(20),
    RegularExpression("^(?:(?:[0-9a-fA-F]{2}:){5}[0-9a-fA-F]{2}|(?:[0-9a-fA-F]{2}-){5}[0-9a-fA-F]{2}|[0-9a-fA-F]{12})$",
        ErrorMessage ="Mac is InValid")] string MAC,
    [MaxLength(100)] string CPU,
    [MaxLength(100)] string GPU,
    [MaxLength(8)] string RAMTotal);



}

