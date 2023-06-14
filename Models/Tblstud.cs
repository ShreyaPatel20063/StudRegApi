using System;
using System.Collections.Generic;

namespace SutdManagmentSysAPI.Models;

public partial class Tblstud
{
    public int Sid { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Gender { get; set; } = null!;

    public int? Cid { get; set; }

    public string Rno { get; set; } = null!;

    public int Div { get; set; }

    public int Sem { get; set; }

    public decimal Per12 { get; set; }

    public string Add { get; set; } = null!;

    public virtual Tblcourse? CidNavigation { get; set; }
}
