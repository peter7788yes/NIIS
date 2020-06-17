using System;

public partial class Vaccination_RecordM_ApplyRecord_AddVaccine : BasePage
{
    public int CaseUserID = 0;
    public int RecordDataID = 0;
    public string VaccineCode = "";
    public string AppointmentDate = "";
    public string tbAry = "[]";
    public string Agency = "";
    public int AgencyID = 0;

    public Vaccination_RecordM_ApplyRecord_AddVaccine()
    {
        base.AddPower("/Vaccination/RecordM/RegisterData.aspx", MyPowerEnum.新增);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET","POST");
        base.DisableTop(false);

        if (Request.HttpMethod.Equals("POST"))
        {
            if (this.IsPostBack == false)
            {
               
            }

        }
        else
        {
            Response.Write("");
            Response.End();
        }

    }

}