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
            var macs = Db.SQL<MAC>("SELECT r FROM MAC r WHERE r.CEB = ? order by r.SoD DESC, r.Idx", cet);

            foreach (var mac in macs)
            {
                MACsElementJson abc = new MACsElementJson
                {
                    MACoNo = (long)mac.MACoNo,
                    Idx = mac.Idx,
                    SoD = mac.SoD,

                    HPPAd = mac.HPP1Ad,
                    GPPAd = mac.GPP1Ad,

                    HSW = mac.HSW,
                    GSW = mac.GSW,

                    H1W = mac.H1W,
                    G1W = mac.G1W,
                    H2W = mac.H2W,
                    G2W = mac.G2W,
                    H3W = mac.H3W,
                    G3W = mac.G3W,
                    H4W = mac.H4W,
                    G4W = mac.G4W,
                    H5W = mac.H5W,
                    G5W = mac.G5W,
                    H6W = mac.H6W,
                    G6W = mac.G6W,
                    H7W = mac.H7W,
                    G7W = mac.G7W,
                };
                if (mac.SoD == "D")
                {
                    abc.HPPAd = mac.HPP1Ad.Split(' ')[0] + " + " + mac.HPP2Ad.Split(' ')[0];
                    abc.GPPAd = mac.GPP1Ad.Split(' ')[0] + " + " + mac.GPP2Ad.Split(' ')[0];
                }
                MACs.Add(abc);
            }
        }

        void Handle(Input.SaveT A)
        {
            Save(false);
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
                    MAC.H1W = (int)mac.H1W;
                    MAC.H2W = (int)mac.H2W;
                    MAC.H3W = (int)mac.H3W;
                    MAC.H4W = (int)mac.H4W;
                    MAC.H5W = (int)mac.H5W;
                    MAC.G1W = (int)mac.G1W;
                    MAC.G2W = (int)mac.G2W;
                    MAC.G3W = (int)mac.G3W;
                    MAC.G4W = (int)mac.G4W;
                    MAC.G5W = (int)mac.G5W;
                    if (ok)
                        MAC.Drm = "OK";
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
                if (A.Value < 0)
                {
                    A.Value = 0;
                    G1W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        G1W = 11;
                    else
                        G1W = A.Value + 2;
                }
            }
            void Handle(Input.G1W A)
            {
                if (A.Value < 0)
                {
                    A.Value = 0;
                    H1W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        H1W = 11;
                    else
                        H1W = A.Value + 2;
                }
            }

            void Handle(Input.H2W A)
            {
                if (A.Value < 0)
                {
                    A.Value = 0;
                    G2W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        G2W = 11;
                    else
                        G2W = A.Value + 2;
                }
            }
            void Handle(Input.G2W A)
            {
                if (A.Value < 0)
                {
                    A.Value = 0;
                    H2W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        H2W = 11;
                    else
                        H2W = A.Value + 2;
                }
            }

            void Handle(Input.H3W A)
            {
                if (A.Value < 0)
                {
                    A.Value = 0;
                    G3W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        G3W = 11;
                    else
                        G3W = A.Value + 2;
                }
            }
            void Handle(Input.G3W A)
            {
                if (A.Value < 0)
                {
                    A.Value = 0;
                    H3W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        H3W = 11;
                    else
                        H3W = A.Value + 2;
                }
            }

            void Handle(Input.H4W A)
            {
                if (A.Value < 0)
                {
                    A.Value = 0;
                    G4W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        G4W = 11;
                    else
                        G4W = A.Value + 2;
                }
            }
            void Handle(Input.G4W A)
            {
                if (A.Value < 0)
                {
                    A.Value = 0;
                    H4W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        H4W = 11;
                    else
                        H4W = A.Value + 2;
                }
            }

            void Handle(Input.H5W A)
            {
                if (A.Value < 0)
                {
                    A.Value = 0;
                    G5W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        G5W = 11;
                    else
                        G5W = A.Value + 2;
                }
            }
            void Handle(Input.G5W A)
            {
                if (A.Value < 0)
                {
                    A.Value = 0;
                    H5W = 11;
                }
                else
                {
                    if (A.Value <= 9)
                        H5W = 11;
                    else
                        H5W = A.Value + 2;
                }
            }

        }
    }


}
