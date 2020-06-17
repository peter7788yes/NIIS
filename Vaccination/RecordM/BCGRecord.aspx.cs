using System;
using System.Collections.Generic;

public partial class Vaccination_RecordM_BCGRecord : BasePage
{
    public new MyPowerVM AddPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM SearchPower = new MyPowerVM("", default(MyPowerEnum));
    public MyPowerVM UpdatePower = new MyPowerVM("", default(MyPowerEnum));
    public string tbData = "";
    public List<MyPowerVM> PowerList = new List<MyPowerVM>();

    public Vaccination_RecordM_BCGRecord()
    {
        PowerList = base.AddPower("/Vaccination/RecordM/BCGRecord.aspx", MyPowerEnum.查詢, MyPowerEnum.新增, MyPowerEnum.修改);
    }

    protected new void Page_Load(object sender, EventArgs e)
    {
       
        base.AllowHttpMethod("GET");
        base.DisableTop(true);

        SearchPower = base.GetPower(PowerList[0]);
        AddPower = base.GetPower(PowerList[1]);
        UpdatePower = base.GetPower(PowerList[2]);

        UC_OpenSelectSingleOrg.PageUrl = "/Vaccination/RecordM/BCGRecord.aspx";

        //PageCL CL = new PageCL();
        //tbData = CL.GetList(new List<BCGRecordVM>(), "ConnDB", "dbo.usp_RecordM_xGetBCGRecordListByMany",
        //                                 new Dictionary<string, object>()
        //                                 {
        //                                      { "@OrgID", 0 },
        //                                      { "@StatisticalYearStart", 0 },
        //                                      { "@StatisticalYearEnd", 0 },
        //                                      { "@pgNow", 1 },
        //                                      { "@pgSize", 10 }
        //                                });
    }

   
}