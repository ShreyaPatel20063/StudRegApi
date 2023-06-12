using System;
using System.Collections.Generic;

namespace SutdManagmentSysAPI.Models;

public partial class Test
{
    public int Rno { get; set; }

    public string Name { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Branch { get; set; } = null!;

    public string? Stadd { get; set; }
}
