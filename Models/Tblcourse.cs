using System;
using System.Collections.Generic;

namespace SutdManagmentSysAPI.Models;

public partial class Tblcourse
{
    public int Cid { get; set; }

    public string Cname { get; set; } = null!;

    public int? Cyears { get; set; }

    public virtual ICollection<Tblstud> Tblstuds { get; set; } = new List<Tblstud>();
}
