using System;

public partial class Vaccination_ParameterM_ApplyRecord_SelectAgency : BasePage
{
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";

    public Vaccination_ParameterM_ApplyRecord_SelectAgency()
    {
        SearchPower = base.AddPower("/Vaccination/ParameterM/RegisterData.aspx", MyPowerEnum.查詢);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET", "POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            //PageCL CL = new PageCL();
            //tbData = CL.GetList(new List<AgencyInfoVM>(), "ConnDB", "dbo.usp_ParameterM_xGetAgencyListByMany",
            //                                 new Dictionary<string, object>()
            //                                 {
            //                                  { "@AgencyCounty", 0 },
            //                                  { "@AgencyTown", 0 },
            //                                  { "@AgencyVillage", 0 },
            //                                  { "@AgencyState", 0 },
            //                                  { "@AgencyName", "" },
            //                                  { "@pgNow", 1 },
            //                                  { "@pgSize", 10 }
            //                                });
        }
        else
        {
            Response.Write("");
            Response.End();
        }
    }
}