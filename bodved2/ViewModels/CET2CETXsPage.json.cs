using BDB2;
using Starcounter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bodved2.ViewModels
{
    partial class CET2CETXsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            IsOS = true;
            IsML = true;

            CET cet = Db.FromId<CET>((ulong)CEToNo);
            var session = Session.Ensure();
            var mp = session.Store[nameof(MasterPage)] as MasterPage;
            IsYetkili = false;
            if (!string.IsNullOrEmpty(cet.CC.Pwd) && mp.Pwd == cet.CC.Pwd)
                IsYetkili = true;

            //IsYetkili = true;   // DENEME

            //Read();
        }

        private void Read()
        {
            // Okusun ama islem yapamasin
            //if (!IsYetkili)   
            //    return;

            CET cet = Db.FromId<CET>((ulong)CEToNo);
            if (H_G == "H")
            {
                CToNo = (long)cet.HCToNo;
                IsOS = cet.IsHOS;
            }
            else if (H_G == "G")
            {
                CToNo = (long)cet.GCToNo;
                IsOS = cet.IsGOS;
            }
            else
                return;

            //IsML = cet.IsHOS && cet.IsGOS && !cet.IsMLY;
            IsML = true;
            if (cet.IsHOS && cet.IsGOS)
                IsML = cet.IsMLY;

            CT ct = Db.FromId<CT>((ulong)CToNo);
            // Lig Takim Event/Musabaka Sng/Dbl Mac sayilari
            SngMacSay = cet.CC.TNSM;
            DblMacSay = cet.CC.TNDM;

            var cetxs = Db.SQL<CETX>("SELECT r FROM CETX r WHERE r.CET = ? and r.CT = ? order by r.SngIdx, r.Idx", cet, ct)
                .OrderBy(x => { int i = x.Idx2; if (i == 0) i = 99; return i; });

            CETXs.Clear();
            foreach (var cetx in cetxs)
            {
                CETXsElementJson abc = new CETXsElementJson
                {
                    CETXoNo = (long)cetx.CETXoNo,
                    CToNo = (long)cetx.CToNo,
                    Idx = cetx.Idx,
                    Idx2 = cetx.Idx2,
                    SH = "H",
                    PPoNo = (long)cetx.PPoNo,
                    PPAd = cetx.PPAd,
                    SngIdx = cetx.SngIdx,
                    DblIdx = cetx.DblIdx,
                    SngIdxS = cetx.SngIdx.ToString("#"),
                    DblIdxS = cetx.DblIdx.ToString("#"),
                };
                if (abc.SngIdx != 0)
                {
                    if ((abc.Idx2 - 2) > abc.SngIdx || (abc.Idx2 + 2) < abc.SngIdx)
                        abc.SH = "E";   // SiralamaHatasi bildirim
                }
                CETXs.Add(abc);
            }
        }

        public void Handle(Input.HOST Action)
        {
            GOST = 0;
            if (Action.Value == 1)
            {
                H_G = "H";
                Read();
            }
        }
        public void Handle(Input.GOST Action)
        {
            HOST = 0;
            if (Action.Value == 1)
            {
                H_G = "G";
                Read();
            }
        }

        public void Handle(Input.CreateT Action)
        {
            H.CETX_Insert(CEToNo, H_G);
            Read();
            //H.CETX_Insert(5468, "G");
        }

        public void Handle(Input.CancelT Action)
        {
                if (this.Parent is CC2CETsPage prnt)
                {
                    prnt.DialogOpened = false;
                    prnt.DialogPage = null;
                }
                else if (this.Parent is CurEventsPage prnt2)
                {
                    prnt2.DialogOpened = false;
                    prnt2.DialogPage = null;
                }


                //p.DialogOpened = false;
                //p.DialogPage = null;
        }

        public void Handle(Input.SaveT Action)
        {
            Save();
            Read();
        }
        public void Save()
        {
            CETX cetx = null;

            int nSngIdx = SngIdxCheck();
            int nDblIdx = DblIdxCheck();

            Hata = "";

            if (nSngIdx != SngMacSay || nDblIdx != DblMacSay)
                Hata = "Sng/Dbl Sýralama Hatasý";

            Db.TransactAsync(() =>
            {
                int i = 1;
                var Xs = CETXs.OrderBy(x => x.Idx);
                foreach (var x in Xs)
                {
                    if (x.SngIdx != 0)
                        x.Idx2 = i++;
                    else
                        x.Idx2 = 0;
                }

                foreach (var org in CETXs)
                {
                    cetx = Db.FromId<CETX>((ulong)org.CETXoNo);
                    cetx.Idx2 = (int)org.Idx2;
                    cetx.SngIdx = (int)org.SngIdx;
                    cetx.DblIdx = (int)org.DblIdx;
                }
            });
        }

        public void Handle(Input.MacCreateT Action)
        {
            H.MAC_Insert(CEToNo);
            IsML = true;
        }

        public void Handle(Input.ConfirmT Action)
        {
            Save();

            int nSngIdx = SngIdxCheck();
            int nDblIdx = DblIdxCheck();

            Hata = "";

            if (nSngIdx == SngMacSay && nDblIdx == DblMacSay)
            {
                CET cet = Db.FromId<CET>((ulong)CEToNo);
                Db.TransactAsync(() =>
                {
                    if (H_G == "H")
                        cet.IsHOS = true;
                    else
                        cet.IsGOS = true;
                });
                Read();
            }
            else
            {
                Hata = "Sng/Dbl Sýralama Hatasý. ONAYLANMADI";
            }
        }

        private int SngIdxCheck()
        {
            // return -1 : birden cok ayni SngIdx var else SngIdx adet (6 veya 8 olmali)
            HashSet<long> hs = new HashSet<long>(); // SngIdx unique olmali ve sirali olmali

            // SngIdx 1..8 tek olmali
            foreach (var org in CETXs)
            {
                if (org.SngIdx != 0)
                {
                    if (hs.Contains(org.SngIdx))
                        return -1;
                    else
                        hs.Add(org.SngIdx);

                }
            }
            return hs.Count;
        }

        private int DblIdxCheck()
        {
            // return -1 : 2den cok ayni DblIdx var else DblIdx adet (6 veya 8 olmali)
            Dictionary<long, int> dct = new Dictionary<long, int>();

            // DblIdx 1..4 cift olmali
            foreach (var org in CETXs)
            {
                if (org.DblIdx != 0)
                {
                    if (!dct.ContainsKey(org.DblIdx))
                        dct[org.DblIdx] = 1;
                    else
                    {
                        if (dct[org.DblIdx] > 1)
                            return -1;
                        dct[org.DblIdx] += 1;
                    }
                }
            }
            return dct.Count;
        }

        [CET2CETXsPage_json.CETXs]
        public partial class CETXsElementJson
        {
            void Handle(Input.SngIdxS Action)
            {
                var p = this.Parent.Parent as CET2CETXsPage;
                if (Int32.TryParse(Action.Value, out int a))
                {
                    if (a < 1 || a > p.SngMacSay)
                        Action.Cancel();
                    else
                        SngIdx = a;
                }
                else
                {
                    Action.Value = "";
                    SngIdx = 0;
                }
            }

            void Handle(Input.DblIdxS Action)
            {
                var p = this.Parent.Parent as CET2CETXsPage;
                if (Int32.TryParse(Action.Value, out int a))
                {
                    if (a < 1 || a > p.DblMacSay)
                        Action.Cancel();
                    else
                        DblIdx = a;
                }
                else
                {
                    Action.Value = "";
                    DblIdx = 0;
                }
            }
            /*
            void Handle(Input.DblIdx Action)
            {
                var p = this.Parent.Parent as CET2CETXsPage;
                if (Action.Value < 0 || Action.Value > p.DblMacSay)
                    Action.Cancel();
            }
            */
        }


    }
}
