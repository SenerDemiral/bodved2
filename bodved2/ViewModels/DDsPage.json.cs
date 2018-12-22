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

        /*
        void Handle(Input.DlgInsertTrigger action)
        {
         //   DlgOpened = true;
            
            var dd = new DD()
            {
                Ad = "new",
                Dnm = 20,
                Info = "info"
            };
            NewNo = (long)dd.GetObjectNo();
            action.Value = NewNo;
            Dnm = Dnm + 1;
            //AttachedScope.Commit();
            //DDs.Data = Db.SQL<DD>("SELECT r FROM DD r");
            Data = null;
            
            // Ise yaramiyor
            Session.RunTaskForAll((s, id) =>
            {
                s.CalculatePatchAndPushOnWebSocket();
            });
            
        }

        void Handle(Input.SaveTrigger action)
        {
            //DDs[0].Ad = "yyyyy";
            AttachedScope.Commit();
            //Data = null;
            
            Session.RunTaskForAll((s, id) =>
            {
                s.CalculatePatchAndPushOnWebSocket();
            });
        }
        */
        void Handle(Input.DlgOpenTrigger action)
        {
            DlgOpened = true;
        }

        void Handle(Input.DlgRejectTrigger A)
        {
            DlgOpened = false;
        }

        void Handle(Input.DlgInsertTrigger Action)
        {
            if (!string.IsNullOrWhiteSpace(DD.Ad))
            {
                Db.Transact(() =>
                {
                    new DD()
                    {
                        Ad = DD.Ad,
                        Dnm = (int)DD.Dnm,
                        Info = DD.Info,
                    };
                });
                DD.ONo = 0;

                //PushChanges();
            }
            DlgOpened = false;
        }

        void Handle(Input.DlgUpdateTrigger Action)
        {
            if (DD.ONo != 0)
            {
                Db.Transact(() =>
                {
                    var r = Db.FromId<DD>((ulong)DD.ONo);
                    r.Ad = DD.Ad;
                    r.Dnm = (int)DD.Dnm;
                    r.Info = DD.Info;
                });
                DD.ONo = 0;

                //PushChanges();
            }
            DlgOpened = false;
        }

        void Handle(Input.DlgDeleteTrigger Action)
        {
            if (DD.ONo != 0)
            {
                Db.Transact(() =>
                {
                    var r = Db.FromId<DD>((ulong)DD.ONo);
                    r.Delete();
                });
                DD.ONo = 0;

                //PushChanges();
            }
            DlgOpened = false;
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

        void Handle(Input.EditTrigger Action)
        {
            var p = this.Parent.Parent as DDsPage;
            p.DD.ONo = DDoNo;
            p.DD.Ad = Ad;
            p.DD.Dnm = Dnm;
            p.DD.Info = Info;

            p.DlgOpened = true;
        }

    }


}
