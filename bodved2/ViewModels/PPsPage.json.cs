﻿using Starcounter;
using BDB2;
using System.Collections.Generic;
using System.Linq;

namespace bodved2.ViewModels
{
    partial class PPsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            PPRD pprd = null;
            bool isRun = false;
            PPsElementJson ppse;
            int TopPP = 0;
            int RunPP = 0;
            string info = "";

            var pps = Db.SQL<PP>("select r from PP r order by r.Ad");
            foreach (var pp in pps)
            {
                TopPP++;
                isRun = false;

                info = "";
                pprd = Db.SQL<PPRD>("select r from PPRD r where r.PP = ? and r.Dnm = ?", pp, H.DnmRun).FirstOrDefault();
                if (pprd != null)
                {
                    isRun = true;
                    RunPP++;
                    info = pprd.CurRuns;
                }

                ppse = new PPsElementJson
                {
                    PPoNo = (long)pp.GetObjectNo(),
                    Ad = pp.Ad,
                    RnkBaz = $"{pp.RnkBaz:#}",
                    IsRun = isRun,
                    CurRuns = info
                };

                PPs.Add(ppse);
            }

            DD dd = Db.SQL<DD>("select r from DD r where r.Dnm = ?", H.DnmRun).FirstOrDefault();

            Hdr = $"Oyuncular ► Toplam {TopPP:n0} ► Son Dönem ► Kayıtlı {RunPP:n0} ♦ Oynamış {dd.ONC:n0}";

        }
    }
}