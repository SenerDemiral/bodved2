using BDB2;
using Starcounter;
using System.Collections.Generic;
using System.Linq;

namespace bodved2.ViewModels
{
    partial class DDsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            Hdr = "BODVED Aktiviteleri";
            int topMac = 0;
            int topSet = 0;
            int topSayi = 0;

            DDs.Data = Db.SQL<DD>("SELECT r FROM DD r order by r.Dnm DESC");

            foreach (var aa in DDs)
            {
                var dd = Db.FromId<DD>((ulong)aa.DDoNo);
                aa.KOC_ = $"{dd.KOC:n0}";
                aa.OOC_ = $"{dd.OOC:n0}";
                aa.Mac = $"{dd.SMC + dd.DMC:n0}";
                aa.Set = $"{dd.SSC + dd.DSC:n0}";
                aa.Sayi = $"{dd.SNC + dd.DNC:n0}";

                topMac += dd.SMC + dd.DMC;
                topSet += dd.SSC + dd.DSC;
                topSayi += dd.SNC + dd.DNC;
            }

            /*
            var dds = Db.SQL<DD>("SELECT r FROM DD r order by r.Dnm DESC");
            foreach (var dd in dds)
            {
                DDs.Add(new DDsElementJson
                {
                    DDoNo = (long)dd.GetObjectNo(),
                    Dnm = dd.Dnm,
                    Ad = dd.Ad,
                    Info = dd.Info,
                    
                    KOC = $"{dd.KOC:n0}",
                    OOC = $"{dd.OOC:n0}",
                    Mac = $"{dd.SMC + dd.DMC:n0}",
                    Set = $"{dd.SSC + dd.DSC:n0}",
                    Sayi = $"{dd.SNC + dd.DNC:n0}"
                });

                topMac += dd.SMC + dd.DMC;
                topSet += dd.SSC + dd.DSC;
                topSayi += dd.SNC + dd.DNC;
            }
            */
            HashSet<ulong> ppHS = new HashSet<ulong>(); // Toplam Oynamıs Uniqe Oyuncu
            var macs = Db.SQL<MAC>("select r from MAC r");
            foreach (var mac in macs)
            {
                ppHS.Add(mac.HPP1oNo);     // Oynuyor
                ppHS.Add(mac.GPP1oNo);     // Oynuyor
                if (mac.SoD == "D")
                {
                    ppHS.Add(mac.HPP2oNo);     // Oynuyor
                    ppHS.Add(mac.GPP2oNo);     // Oynuyor
                }
            }
            
            long topPP = Db.SQL<long>("select count(r) from PP r").FirstOrDefault();

            TopPP = $"{topPP:n0}";
            RunPP = $"{ppHS.Count():n0}";
            TopMac = $"{topMac:n0}";
            TopSet = $"{topSet:n0}";
            TopSayi = $"{topSayi:n0}";
        }
    }
    
    [DDsPage_json.DD]
    partial class DDPartial : Json
    {
        void Handle(Input.NewTrgr action)
        {
            ONo = 0;
            Ad = "";
            Dnm = 0;
            Info = "";
            IsNew = true;
            Opened = true;
            PPoNo = 0;
        }

        void Handle(Input.InsTrgr Action)
        {
            if (!string.IsNullOrWhiteSpace(Ad))
            {
                Db.Transact(() =>
                {
                    new DD()
                    {
                        Ad = Ad,
                        Dnm = (int)Dnm,
                        Info = Info,
                        PP = PPoNo == 0 ? null : Db.FromId<PP>((ulong)PPoNo)
                    };
                });
                ONo = 0;

                //PushChanges();    // Kendisi dahil Diger Clients Insert gormuyor, Refresh gerekli
            }
            Opened = false;
            var p = this.Parent as DDsPage;

            //p.DDs.Data = Db.SQL<DD>("SELECT r FROM DD r order by r.Dnm DESC");
            p.Data = null;

            Session.RunTaskForAll((s, sId) => {
                var cp = (s.Store[nameof(MasterPage)] as MasterPage).CurrentPage;
                if (cp is DDsPage)
                {
                    (s.Store[nameof(MasterPage)] as MasterPage).CurrentPage.Data = null;
                    s.CalculatePatchAndPushOnWebSocket();
                }
            });

        }

        void Handle(Input.UpdTrgr Action)
        {
            if (ONo != 0)
            {
                Db.Transact(() =>
                {
                    var r = Db.FromId<DD>((ulong)ONo);
                    r.Ad = Ad.ToUpper();
                    r.Dnm = (int)Dnm;
                    r.Info = Info;
                    r.PP = PPoNo == 0 ? null : Db.FromId<PP>((ulong)PPoNo);
                });
                ONo = 0;

                Session.RunTaskForAll((s, id) =>
                {
                    s.CalculatePatchAndPushOnWebSocket();
                });
            }
            Opened = false;
        }

        void Handle(Input.DelTrgr Action)
        {
            if (ONo != 0)
            {
                Db.Transact(() =>
                {
                    var r = Db.FromId<DD>((ulong)ONo);
                    r.Delete();
                });
                ONo = 0;

                Session.RunTaskForAll((s, id) =>
                {
                    s.CalculatePatchAndPushOnWebSocket();
                });
            }
            Opened = false;
        }

        void Handle(Input.RejTrgr A)
        {
            Opened = false;
        }
    }

    [DDsPage_json.DDs]
    partial class DDsPartial : Json
    {
        protected override void OnData()
        {
            base.OnData();
            var dd = Db.FromId<DD>((ulong)DDoNo);

            var p = this.Parent.Parent as DDsPage;
            //Mac = SMC + DMC;
            //p.TopSet += DDoNo;

        }

        void Handle(Input.EdtTrgr Action)
        {
            var p = this.Parent.Parent as DDsPage;
            p.DD.ONo = DDoNo;
            p.DD.Ad = Ad;
            p.DD.Dnm = Dnm;
            p.DD.Info = Info;

            p.DD.PPoNo = PPoNo;
            p.DD.IsNew = false; // Edit
            p.DD.Opened = true;
        }
    }


}
