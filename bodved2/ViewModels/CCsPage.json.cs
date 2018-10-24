using Starcounter;
using BDB2;

namespace bodved2.ViewModels
{
    partial class CCsPage : Json
    {
        public CCsPage()
        {

        }

        protected override void OnData()
        {
            base.OnData();

            this.CCs.Data = Db.SQL<CC>("SELECT r FROM CC r order by r.Idx");
        }
    }
}
