﻿using BDB2;
using Starcounter;
using System;
using System.Linq;

namespace bodved2.ViewModels
{
    partial class CurEventsPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            DD dd = Db.SQL<DD>("select r from DD r where r.Dnm = ?", H.DnmRun).FirstOrDefault();
            Hdr = $"{dd.Ad} ► Güncel";

            // Gecmis Pzrtsi'den bir sonraki Cumaya kadar. 2 haftalik 
            DateTime firstMonday = H.GetNextWeekday(DayOfWeek.Monday);
            DateTime T1 = firstMonday.AddDays(-7);  // -7
            DateTime T2 = firstMonday.AddDays(5);

            string tarih = "";


            var cets = Db.SQL<CET>("SELECT r FROM CET r WHERE r.CC.Dnm = ? and r.Drm = ? and r.Trh < ? order by r.Trh", H.DnmRun, "", T1);
            foreach (var cet in cets)
            {
                var rde = new CETsElementJson
                {
                    CCoNo = (long)cet.CCoNo,
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
                    tarih = rde.Tarih;
                }

                CETs.Add(rde);
            }

            cets = Db.SQL<CET>("SELECT r FROM CET r WHERE r.CC.Dnm = ? and r.Trh >= ? and r.Trh <= ? order by r.Trh", H.DnmRun, T1, T2);
            foreach (var cet in cets)
            {
                var rde = new CETsElementJson
                {
                    CCoNo = (long)cet.CCoNo,
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
                    rde.Break = true;
                    tarih = rde.Tarih;
                }

                CETs.Add(rde);
            }
        }

        [CurEventsPage_json.CETs]
        public partial class CETsElementJson
        {
            public void Handle(Input.OST Action)
            {
                var p = this.Parent.Parent as CurEventsPage;
                p.DialogPage = Self.GET<Json>($"/bodved/partials/CET2CETXs/{this.CEToNo}/1");
                p.DialogOpened = true;
            }
        }

    }
}

