﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CaseMaintain_ChooseUserContactList : System.Web.UI.Page
{
    protected string CaseID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        CaseID= Request.Form["i"] ?? ""; 

    } 
}