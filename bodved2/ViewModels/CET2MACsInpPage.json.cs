using BDB2;
using Starcounter;

namespace bodved2.ViewModels
{
    partial class CET2MACsInpPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            CET cet = Db.FromId<CET>((ulong)CEToNo);
            HCTAd = cet.HCTAd;
            GCTAd = cet.GCTAd;

            if (string.IsNullOrEmpty(cet.Drm))
            {
                var session = Session.Ensure();
                var mp = session.Store[nameof(MasterPage)] as MasterPage;
                if (!string.IsNullOrEmpty(cet.CC.Pwd) && mp.Pwd == cet.CC.Pwd)
                    IsEdit = true;
            }
            Read();
        }

        void Read()
        {
            CET cet = Db.FromId<CET>((ulong)CEToNo);
            var macs = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.CEB = ? order by r.SoD DESC, r.Idx DESC", cet);

            MACs.Clear();
            string SoD = "S";
            foreach (var mac in macs)
            {
                MACsElementJson abc = new MACsElementJson
                {
                    MACoNo = (long)mac.MACoNo,
                    Drm = mac.Drm,
                    Idx = mac.Idx,
                    SoD = mac.SoD,

                    HPPAd = mac.HPP1Ad,
                    GPPAd = mac.GPP1Ad,

                    HSW = mac.HSW,
                    GSW = mac.GSW,
                    HWL = mac.HWL,
                    GWL = mac.GWL,

                    H1W = mac.H1W.ToString("#"),
                    G1W = mac.G1W.ToString("#"),
                    H2W = mac.H2W.ToString("#"),
                    G2W = mac.G2W.ToString("#"),
                    H3W = mac.H3W.ToString("#"),
                    G3W = mac.G3W.ToString("#"),
                    H4W = mac.H4W.ToString("#"),
                    G4W = mac.G4W.ToString("#"),
                    H5W = mac.H5W.ToString("#"),
                    G5W = mac.G5W.ToString("#"),
                    H6W = mac.H6W.ToString("#"),
                    G6W = mac.G6W.ToString("#"),
                    H7W = mac.H7W.ToString("#"),
                    G7W = mac.G7W.ToString("#"),
                };
                if (mac.SoD == "D")
                {
                    abc.HPPAd = mac.HPP1Ad.Split(' ')[0] + " + " + mac.HPP2Ad.Split(' ')[0];
                    abc.GPPAd = mac.GPP1Ad.Split(' ')[0] + " + " + mac.GPP2Ad.Split(' ')[0];
                }
                if (abc.SoD != SoD)
                {
                    abc.Break = true;
                    SoD = abc.SoD;
                }

                MACs.Add(abc);
            }
        }


        void Handle(Input.SaveT A)
        {
            Save(false);
            Read();
        }

        void Handle(Input.ConfirmT A)
        {
            Save(true);
        }

        void Save(bool ok)
        {
            MAC MAC = null;

            Db.TransactAsync(() =>
            {
                foreach (var mac in MACs)
                {
                    MAC = Db.FromId<MAC>((ulong)mac.MACoNo);
                    if (mac.Drm == "OK")  // Single MacDrm OK degilse yapma cunki Idx 88 lerin oynamasi yasak, Rnk belli degil
                    {
                        MAC.H1W = H.IntParse(mac.H1W);
                        MAC.H2W = H.IntParse(mac.H2W);
                        MAC.H3W = H.IntParse(mac.H3W);
                        MAC.H4W = H.IntParse(mac.H4W);
                        MAC.H5W = H.IntParse(mac.H5W);
                        MAC.G1W = H.IntParse(mac.G1W);
                        MAC.G2W = H.IntParse(mac.G2W);
                        MAC.G3W = H.IntParse(mac.G3W);
                        MAC.G4W = H.IntParse(mac.G4W);
                        MAC.G5W = H.IntParse(mac.G5W);
                    }
                    if (mac.Drm == "hX")    // Home Diskalifiye
                    {
                        MAC.G1W = 11;
                        MAC.G2W = 11;
                        MAC.G3W = 11;
                    }
                    if (mac.Drm == "gX")    // Guest Diskalifiye
                    {
                        MAC.H1W = 11;
                        MAC.H2W = 11;
                        MAC.H3W = 11;
                    }
                    // mac.Drm == "D" ise yani ikiside Diskalifiye ise zaten sonuclari yazmiyacak ve sifir kalacak
                    H.MAC_RefreshSonuc(MAC);
                }
                if (ok)
                {
                    CET cet = Db.FromId<CET>((ulong)CEToNo);
                    cet.Drm = "OK";
                    IsEdit = false;
                }

            });
            if (ok)
            {
                H.MAC_RefreshSonuc(H.DnmRun);
                H.CET_RefreshSonuc(H.DnmRun);
                H.CT_RefreshSonuc(H.DnmRun);
                H.CTP_RefreshSonuc(H.DnmRun);
                H.DD_RefreshSonuc(H.DnmRun);
                H.PPRD_RefreshSonuc(H.DnmRun);
                H.PPRD_RefreshCurRuns(H.DnmRun);

            }
        }

        [CET2MACsInpPage_json.MACs]
        public partial class MACsElementJson
        {


            void Handle(Input.H1W A)
            {
                var res = H.GetSetSayi(A.Value, G1W);
                A.Value = res.Item1;
                G1W = res.Item2;
            }
            void Handle(Input.G1W A)
            {
                var res = H.GetSetSayi(A.Value, H1W);
                A.Value = res.Item1;
                H1W = res.Item2;
            }

            void Handle(Input.H2W A)
            {
                var res = H.GetSetSayi(A.Value, G2W);
                A.Value = res.Item1;
                G2W = res.Item2;
            }
            void Handle(Input.G2W A)
            {
                var res = H.GetSetSayi(A.Value, H2W);
                A.Value = res.Item1;
                H2W = res.Item2;
            }

            void Handle(Input.H3W A)
            {
                var res = H.GetSetSayi(A.Value, G3W);
                A.Value = res.Item1;
                G3W = res.Item2;
            }
            void Handle(Input.G3W A)
            {
                var res = H.GetSetSayi(A.Value, H3W);
                A.Value = res.Item1;
                H3W = res.Item2;
            }

            void Handle(Input.H4W A)
            {
                var res = H.GetSetSayi(A.Value, G4W);
                A.Value = res.Item1;
                G4W = res.Item2;
            }
            void Handle(Input.G4W A)
            {
                var res = H.GetSetSayi(A.Value, H4W);
                A.Value = res.Item1;
                H4W = res.Item2;
            }

            void Handle(Input.H5W A)
            {
                var res = H.GetSetSayi(A.Value, G5W);
                A.Value = res.Item1;
                G5W = res.Item2;
            }
            void Handle(Input.G5W A)
            {
                var res = H.GetSetSayi(A.Value, H5W);
                A.Value = res.Item1;
                H5W = res.Item2;
            }

        }
    }


}
