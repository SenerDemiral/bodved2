using BDB2;
using Starcounter;

namespace bodved2.ViewModels
{
    partial class CC2CETsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            CC cc = Db.FromId<CC>((ulong)CCoNo);

            var session = Session.Ensure();
            var mp = session.Store[nameof(MasterPage)] as MasterPage;
            IsYetkili = false;
            if (!string.IsNullOrEmpty(cc.Pwd) && mp.Pwd == cc.Pwd)
                IsYetkili = true;

            Hdr = $"{cc.Ad} ► Fikstür";

            string tarih = "";
            var cets = Db.SQL<CET>("SELECT r FROM CET r WHERE r.CC = ? order by r.Trh", cc);
            foreach (var cet in cets)
            {
                var rde = new CETsElementJson
                {
                    CEToNo = (long)cet.CEToNo,

                    HCToNo = (long)cet.HCToNo,
                    HCTAd = cet.HCTAd,
                    GCToNo = (long)cet.GCToNo,
                    GCTAd = cet.GCTAd,
                    Tarih = cet.Tarih,
                    Drm = cet.Drm,

                    HWL = cet.HWL,
                    GWL = cet.GWL,
                    HR = cet.HR,

                    GR = cet.GR,
                    HPW = cet.HPW,
                    GPW = cet.GPW,
                    Puan = cet.Puan,
                    Info = cet.Info,
                    IsHOS = cet.IsHOS,
                    IsGOS = cet.IsGOS
                    
                };

                if (rde.Tarih != tarih)
                {
                    //rde.Break = true; // Ayni tarihdekileri gruplamak icin. Gerek yok
                    tarih = rde.Tarih;
                }

                CETs.Add(rde);
            }
        }

        [CC2CETsPage_json.CETs]
        public partial class CETsElementJson
        {
            public void Handle(Input.OST Action)
            {
                var p = this.Parent.Parent as CC2CETsPage;
                p.DialogPage = Self.GET<Json>($"/bodved/partials/CET2CETXs/{this.CEToNo}/1");
                p.DialogOpened = true;
            }
        }


    }
}
