using System;

public partial class Vaccination_CertificateM_PrintCertificate : BasePage
{
    protected new void Page_Load(object sender, EventArgs e)
    {
        base.AllowHttpMethod("GET");
        base.DisableTop(false);
    }
}