namespace SiparisTakip.Models.Tables
{
    public class Setting
    {
        public int settingId { get; set; }
        public string settingEPosta { get; set; }
        public string settingPassword { get; set; }
        public string settingSmtpHost { get; set; }
        public int settingSmtpPort { get; set; }
        public bool settingSmtpSSL { get; set; }
    }
}