using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using RestLib;
using Starcounter;
using BDB2;

namespace RestStarcounterServer
{
    class CRUDsImpl : RestLib.CRUDs.CRUDsBase
    {
        // Lookuo Players
        public override async Task PPlookUp(QryProxy request, IServerStreamWriter<PPlookUpProxy> responseStream, ServerCallContext context)
        {
            PPlookUpProxy proxy = new PPlookUpProxy();
            List<PPlookUpProxy> proxyList = new List<PPlookUpProxy>();

            Type proxyType = typeof(PPlookUpProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            await Scheduling.RunTask(() =>
            {
                foreach (var row in Db.SQL<PP>("select r from PP r"))
                {
                    proxy = new PPlookUpProxy
                    {
                        RowKey = row.GetObjectNo(),
                        Ad = row.Ad,
                        Sex = row.Sex ?? "",
                        IsRun = row.IsRun,
                    };
                    if (proxy.IsRun)
                    {
                        foreach(var ctp in Db.SQL<CTP>("select r from CTP r where r.PP = ?", row))
                        {
                            if (ctp.IsRun)
                                proxy.CTs += "<" + ctp.CT.GetObjectNo() + ">";
                        }
                    }

                    proxyList.Add(proxy);
                }
            });

            foreach (var p in proxyList)
            {
                await responseStream.WriteAsync(p);
            }
        }

        // Players
        public override async Task PPFill(QryProxy request, IServerStreamWriter<PPProxy> responseStream, ServerCallContext context)
        {
            PPProxy proxy = new PPProxy();
            List<PPProxy> proxyList = new List<PPProxy>();

            Type proxyType = typeof(PPProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            await Scheduling.RunTask(() =>
            {
                foreach (var row in Db.SQL<PP>("select r from PP r"))
                {
                    //proxy = ReflectionExample.ToProxy<AHPproxy, AHP>(row);

                    proxy = new PPProxy
                    {
                        RowKey = row.GetObjectNo(),
                        Ad = row.Ad,
                        Sex = row.Sex ?? "",
                        Tel = row.Tel ?? "",
                        Info = row.Info ?? "",
                        IsRun = row.IsRun,

                        RnkBaz = row.RnkBaz,
                        RnkSon = row.RnkSon,
                        RnkIdx = row.RnkIdx,

                        SST = row.SST,
                        SSW = row.SSW,
                        SSL = row.SSL,
                        SMT = row.SMT,
                        SMW = row.SMW,
                        SML = row.SML,
                        DST = row.DST,
                        DSW = row.DSW,
                        DSL = row.DSL,
                        DMT = row.DMT,
                        DMW = row.DMW,
                        DML = row.DML,

                    };

                    proxyList.Add(proxy);
                }
            });

            foreach (var p in proxyList)
            {
                await responseStream.WriteAsync(p);
            }
        }
        public override Task<PPProxy> PPUpdate(PPProxy request, ServerCallContext context)
        {
            Scheduling.RunTask(() =>
            {
                // RowSte: Added, Modified, Deletede, Unchanged
                Db.Transact(() =>
                {
                    if (request.RowSte == "A" || request.RowSte == "M")
                    {
                        if (request.RowErr == string.Empty)
                        {
                            PP row = CRUDsHelper.FromProxy<PPProxy, PP>(request);
                            //XUT.Append(request.RowUsr, row, request.RowSte);
                            request = CRUDsHelper.ToProxy<PPProxy, PP>(row);
                        }

                    }
                    else if (request.RowSte == "D")
                    {
                        var row = (PP)Db.FromId(request.RowKey);
                        if (row == null)
                        {
                            request.RowErr = "PP Rec not found";
                        }
                        else
                        {
                            request.RowErr = $"Silemezsiniz";
                        }
                    }
                });
            }).Wait();

            return Task.FromResult(request);
        }

        // Competitions
        public override async Task CCFill(QryProxy request, IServerStreamWriter<CCProxy> responseStream, ServerCallContext context)
        {
            CCProxy proxy = new CCProxy();
            List<CCProxy> proxyList = new List<CCProxy>();

            Type proxyType = typeof(CCProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            await Scheduling.RunTask(() =>
            {
                foreach (var row in Db.SQL<CC>("select r from CC r"))
                {
                    proxy = CRUDsHelper.ToProxy<CCProxy, CC>(row);
                    /*
                    proxy = new CCProxy
                    {
                        RowKey = row.GetObjectNo(),
                        Ad = row.Ad,
                        Skl = row.Skl ?? "",
                        Grp = row.Grp ?? "",
                        Info = row.Info ?? "",
                        IsRun = row.IsRun,
                        IsRnkd = row.IsRnkd,
                    };
                    */
                    proxyList.Add(proxy);
                }
            });

            foreach (var p in proxyList)
            {
                await responseStream.WriteAsync(p);
            }
        }
        public override Task<CCProxy> CCUpdate(CCProxy request, ServerCallContext context)
        {
            Scheduling.RunTask(() =>
            {
                // RowSte: Added, Modified, Deletede, Unchanged
                Db.Transact(() =>
                {
                    if (request.RowSte == "A" || request.RowSte == "M")
                    {
                        if (request.RowErr == string.Empty)
                        {
                            CC row = CRUDsHelper.FromProxy<CCProxy, CC>(request);
                            //XUT.Append(request.RowUsr, row, request.RowSte);
                            request = CRUDsHelper.ToProxy<CCProxy, CC>(row);
                        }

                    }
                    else if (request.RowSte == "D")
                    {
                        var row = (CC)Db.FromId(request.RowKey);
                        if (row == null)
                        {
                            request.RowErr = "CC Rec not found";
                        }
                        else
                        {
                            request.RowErr = $"Silemezsiniz";
                        }
                    }
                });
            }).Wait();

            Session.RunTaskForAll((s, id) =>
            {
                s.CalculatePatchAndPushOnWebSocket();
            });

            return Task.FromResult(request);
        }

        // Teams
        public override async Task CTFill(QryProxy request, IServerStreamWriter<CTProxy> responseStream, ServerCallContext context)
        {
            CTProxy proxy = new CTProxy();
            List<CTProxy> proxyList = new List<CTProxy>();

            Type proxyType = typeof(CTProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            await Scheduling.RunTask(() =>
            {
                IEnumerable<CT> rows = null;
                if (request.Query == "CC")
                    rows = Db.SQL<CT>("select r from CT r where r.CC.ObjectNo = ?", ulong.Parse(request.Param));
                else
                    rows = Db.SQL<CT>("select r from CT r ");

                foreach (var row in rows)
                {
                    //proxy = ReflectionExample.ToProxy<AHPproxy, AHP>(row);
                    proxy = CRUDsHelper.ToProxy<CTProxy, CT>(row);
                    /*
                    proxy = new CTProxy
                    {
                        RowKey = row.GetObjectNo(),
                        CC = row.CC == null ? 0 : row.CC.GetObjectNo(),
                        K1 = row.K1 == null ? 0 : row.K1.GetObjectNo(),
                        K2 = row.K2 == null ? 0 : row.K1.GetObjectNo(),

                        Ad = row.Ad,
                        Adres = row.Adres ?? "",
                        Info = row.Info ?? "",
                        NG = row.NG,
                        NM = row.NM,
                        NB = row.NB,
                        NT = row.NT,
                        NX = row.NX,
                        KA = row.KA,
                        KV = row.KV,
                        KF = row.KF,
                        PW = row.PW,
                        
                        IsRun = row.IsRun,
                    };
                    */
                    proxyList.Add(proxy);
                }
            });

            foreach (var p in proxyList)
            {
                await responseStream.WriteAsync(p);
            }
        }
        public override Task<CTProxy> CTUpdate(CTProxy request, ServerCallContext context)
        {
            Scheduling.RunTask(() =>
            {
                // RowSte: Added, Modified, Deletede, Unchanged
                Db.Transact(() =>
                {
                    if (request.RowSte == "A" || request.RowSte == "M")
                    {
                        if (request.RowErr == string.Empty)
                        {
                            CT row = CRUDsHelper.FromProxy<CTProxy, CT>(request);
                            //XUT.Append(request.RowUsr, row, request.RowSte);
                            request = CRUDsHelper.ToProxy<CTProxy, CT>(row);
                        }

                    }
                    else if (request.RowSte == "D")
                    {
                        var row = Db.FromId(request.RowKey) as CT;
                        if (row == null)
                        {
                            request.RowErr = "CT Rec not found";
                        }
                        else
                        {
                            // CTP ve CET detaylari yoksa sil.
                            var ctp = Db.SQL("select r from CTP r where r.CT.ObjectNo = ?", request.RowKey).FirstOrDefault();
                            if(ctp == null)
                            {
                                var cet = Db.SQL("select r from CET r where r.HCT.ObjectNo = ? or r.GCT.ObjectNo = ?", request.RowKey, request.RowKey).FirstOrDefault();
                                if(cet == null)
                                    row.Delete();
                                else
                                    request.RowErr = $"Event var, Silemezsiniz";
                            }
                            else 
                                request.RowErr = $"Oyuncuları var, Silemezsiniz";
                        }
                    }
                });
            }).Wait();

            Session.RunTaskForAll((s, id) =>
            {
                s.CalculatePatchAndPushOnWebSocket();
            });

            return Task.FromResult(request);
        }

        // Team Players
        public override async Task CTPFill(QryProxy request, IServerStreamWriter<CTPProxy> responseStream, ServerCallContext context)
        {
            CTPProxy proxy = new CTPProxy();
            List<CTPProxy> proxyList = new List<CTPProxy>();

            Type proxyType = typeof(CTPProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            await Scheduling.RunTask(() =>
            {
                IEnumerable<CTP> rows = null;
                if (request.Query == "CT")
                    rows = Db.SQL<CTP>("select r from CTP r where r.CT.ObjectNo = ?", ulong.Parse(request.Param));
                else
                    rows = Db.SQL<CTP>("select r from CTP r ");

                foreach (var row in rows)
                {
                    proxy = CRUDsHelper.ToProxy<CTPProxy, CTP>(row);
                    /*
                    proxy = new CTProxy
                    {
                        RowKey = row.GetObjectNo(),
                        CC = row.CC == null ? 0 : row.CC.GetObjectNo(),
                        K1 = row.K1 == null ? 0 : row.K1.GetObjectNo(),
                        K2 = row.K2 == null ? 0 : row.K1.GetObjectNo(),

                        Ad = row.Ad,
                        Adres = row.Adres ?? "",
                        Info = row.Info ?? "",
                        NG = row.NG,
                        NM = row.NM,
                        NB = row.NB,
                        NT = row.NT,
                        NX = row.NX,
                        KA = row.KA,
                        KV = row.KV,
                        KF = row.KF,
                        PW = row.PW,
                        
                        IsRun = row.IsRun,
                    };
                    */
                    proxyList.Add(proxy);
                }
            });

            foreach (var p in proxyList)
            {
                await responseStream.WriteAsync(p);
            }
        }
        public override Task<CTPProxy> CTPUpdate(CTPProxy request, ServerCallContext context)
        {
            Scheduling.RunTask(() =>
            {
                // RowSte: Added, Modified, Deletede, Unchanged
                Db.Transact(() =>
                {
                    if (request.RowSte == "A" || request.RowSte == "M")
                    {
                        if (request.RowErr == string.Empty)
                        {
                            CTP row = CRUDsHelper.FromProxy<CTPProxy, CTP>(request);
                            //XUT.Append(request.RowUsr, row, request.RowSte);
                            request = CRUDsHelper.ToProxy<CTPProxy, CTP>(row);
                        }

                    }
                    else if (request.RowSte == "D")
                    {
                        var row = (CTP)Db.FromId(request.RowKey);
                        if (row == null)
                        {
                            request.RowErr = "CTP Rec not found";
                        }
                        else
                        {
                            request.RowErr = $"Silemezsiniz";
                        }
                    }
                });
            }).Wait();

            Session.RunTaskForAll((s, id) =>
            {
                s.CalculatePatchAndPushOnWebSocket();
            });

            return Task.FromResult(request);
        }

        // Team Events
        public override async Task CETFill(QryProxy request, IServerStreamWriter<CETProxy> responseStream, ServerCallContext context)
        {
            CETProxy proxy = new CETProxy();
            List<CETProxy> proxyList = new List<CETProxy>();

            Type proxyType = typeof(CETProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            await Scheduling.RunTask(() =>
            {
                IEnumerable<CET> rows = null;
                if (request.Query == "CC")
                    rows = Db.SQL<CET>("select r from CET r where r.CC.ObjectNo = ?", ulong.Parse(request.Param));
                else
                    rows = Db.SQL<CET>("select r from CET r");

                foreach (var row in rows)
                {
                    //proxy = ReflectionExample.ToProxy<AHPproxy, AHP>(row);
                    proxy = CRUDsHelper.ToProxy<CETProxy, CET>(row);
                    /*
                    proxy = new CTProxy
                    {
                        RowKey = row.GetObjectNo(),
                        CC = row.CC == null ? 0 : row.CC.GetObjectNo(),
                        K1 = row.K1 == null ? 0 : row.K1.GetObjectNo(),
                        K2 = row.K2 == null ? 0 : row.K1.GetObjectNo(),

                        Ad = row.Ad,
                        Adres = row.Adres ?? "",
                        Info = row.Info ?? "",
                        NG = row.NG,
                        NM = row.NM,
                        NB = row.NB,
                        NT = row.NT,
                        NX = row.NX,
                        KA = row.KA,
                        KV = row.KV,
                        KF = row.KF,
                        PW = row.PW,
                        
                        IsRun = row.IsRun,
                    };
                    */
                    proxyList.Add(proxy);
                }
            });

            foreach (var p in proxyList)
            {
                await responseStream.WriteAsync(p);
            }
        }
        public override Task<CETProxy> CETUpdate(CETProxy request, ServerCallContext context)
        {
            Scheduling.RunTask(() =>
            {
                // RowSte: Added, Modified, Deletede, Unchanged
                Db.Transact(() =>
                {
                    if (request.RowSte == "A" || request.RowSte == "M")
                    {
                        if (request.RowErr == string.Empty)
                        {
                            CET row = CRUDsHelper.FromProxy<CETProxy, CET>(request);
                            //XUT.Append(request.RowUsr, row, request.RowSte);
                            request = CRUDsHelper.ToProxy<CETProxy, CET>(row);
                        }

                    }
                    else if (request.RowSte == "D")
                    {
                        var row = (CET)Db.FromId(request.RowKey);
                        if (row == null)
                        {
                            request.RowErr = "CET Rec not found";
                        }
                        else
                        {
                            var mac = Db.SQL<MAC>("select r from MAC r where r.CEB.ObjectNo = ?", request.RowKey).FirstOrDefault();
                            if (mac != null)
                                request.RowErr = $"Maçları var, Silemezsiniz";
                            else
                                row.Delete();
                        }
                    }
                });
            }).Wait();

            Session.RunTaskForAll((s, id) =>
            {
                s.CalculatePatchAndPushOnWebSocket();
            });

            return Task.FromResult(request);
        }

        // Ferdi Players
        public override async Task CFFill(QryProxy request, IServerStreamWriter<CFProxy> responseStream, ServerCallContext context)
        {
            CFProxy proxy = new CFProxy();
            List<CFProxy> proxyList = new List<CFProxy>();

            Type proxyType = typeof(CFProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            await Scheduling.RunTask(() =>
            {
                IEnumerable<CF> rows = null;
                if (request.Query == "CC")
                    rows = Db.SQL<CF>("select r from CF r where r.CC.ObjectNo = ?", ulong.Parse(request.Param));
                else
                    rows = Db.SQL<CF>("select r from CF r");

                foreach (var row in rows)
                {
                    proxy = CRUDsHelper.ToProxy<CFProxy, CF>(row);
                    proxyList.Add(proxy);
                }
            });

            foreach (var p in proxyList)
            {
                await responseStream.WriteAsync(p);
            }
        }
        public override Task<CFProxy> CFUpdate(CFProxy request, ServerCallContext context)
        {
            Scheduling.RunTask(() =>
            {
                // RowSte: Added, Modified, Deletede, Unchanged
                Db.Transact(() =>
                {
                    if (request.RowSte == "A" || request.RowSte == "M")
                    {
                        if (request.RowErr == string.Empty)
                        {
                            CF row = CRUDsHelper.FromProxy<CFProxy, CF>(request);
                            //XUT.Append(request.RowUsr, row, request.RowSte);
                            request = CRUDsHelper.ToProxy<CFProxy, CF>(row);
                        }

                    }
                    else if (request.RowSte == "D")
                    {
                        var row = (CF)Db.FromId(request.RowKey);
                        if (row == null)
                        {
                            request.RowErr = "CF Rec not found";
                        }
                        else
                        {
                            request.RowErr = $"Silemezsiniz";
                        }
                    }
                });
            }).Wait();

            Session.RunTaskForAll((s, id) =>
            {
                s.CalculatePatchAndPushOnWebSocket();
            });

            return Task.FromResult(request);
        }

        // Ferdi Events
        public override async Task CEFFill(QryProxy request, IServerStreamWriter<CEFProxy> responseStream, ServerCallContext context)
        {
            CEFProxy proxy = new CEFProxy();
            List<CEFProxy> proxyList = new List<CEFProxy>();

            Type proxyType = typeof(CEFProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            await Scheduling.RunTask(() =>
            {
                IEnumerable<CEF> rows = null;
                if (request.Query == "CC")
                    rows = Db.SQL<CEF>("select r from CEF r where r.CC.ObjectNo = ?", ulong.Parse(request.Param));
                else
                    rows = Db.SQL<CEF>("select r from CEF r");

                foreach (var row in rows)
                {
                    proxy = CRUDsHelper.ToProxy<CEFProxy, CEF>(row);
                    proxyList.Add(proxy);
                }
            });

            foreach (var p in proxyList)
            {
                await responseStream.WriteAsync(p);
            }
        }
        public override Task<CEFProxy> CEFUpdate(CEFProxy request, ServerCallContext context)
        {
            Scheduling.RunTask(() =>
            {
                // RowSte: Added, Modified, Deletede, Unchanged
                Db.Transact(() =>
                {
                    if (request.RowSte == "A" || request.RowSte == "M")
                    {
                        if (request.RowErr == string.Empty)
                        {
                            CEF row = CRUDsHelper.FromProxy<CEFProxy, CEF>(request);
                            //XUT.Append(request.RowUsr, row, request.RowSte);
                            request = CRUDsHelper.ToProxy<CEFProxy, CEF>(row);
                        }

                    }
                    else if (request.RowSte == "D")
                    {
                        var row = (CEF)Db.FromId(request.RowKey);
                        if (row == null)
                        {
                            request.RowErr = "CEF Rec not found";
                        }
                        else
                        {
                            var mac = Db.SQL<MAC>("select r from MAC r where r.CEB.ObjectNo = ?", request.RowKey).FirstOrDefault();
                            if (mac != null)
                                request.RowErr = $"Maçı var, Silemezsiniz";
                            else
                                row.Delete();
                        }
                    }
                });
            }).Wait();

            Session.RunTaskForAll((s, id) =>
            {
                s.CalculatePatchAndPushOnWebSocket();
            });

            return Task.FromResult(request);
        }

        // Maclar
        public override async Task MACFill(QryProxy request, IServerStreamWriter<MACProxy> responseStream, ServerCallContext context)
        {
            MACProxy proxy = new MACProxy();
            List<MACProxy> proxyList = new List<MACProxy>();

            Type proxyType = typeof(MACProxy);
            PropertyInfo[] proxyProperties = proxyType.GetProperties().Where(x => x.CanRead && x.CanWrite).ToArray();

            await Scheduling.RunTask(() =>
            {
                IEnumerable<MAC> rows = null;// = Db.SQL<MAC>("select r from MAC r");
                if (request.Query == "CET")
                    rows = Db.SQL<MAC>("select r from MAC r where r.CEB.ObjectNo = ?", ulong.Parse(request.Param));
                else if (request.Query == "CEF")
                    rows = Db.SQL<MAC>("select r from MAC r where r.CEB.ObjectNo = ?", ulong.Parse(request.Param));
                else if (request.Query == "CC")
                    rows = Db.SQL<MAC>("select r from MAC r where r.CC.ObjectNo = ?", ulong.Parse(request.Param));
                else if (request.Query == "PP")
                {
                    ulong pp = ulong.Parse(request.Param);
                    rows = Db.SQL<MAC>("select r from MAC r where r.HPP1.ObjectNo = ? or r.HPP2.ObjectNo = ? or r.GPP1.ObjectNo = ? or r.GPP2.ObjectNo = ?", pp, pp, pp, pp);
                }
                else
                    rows = Db.SQL<MAC>("select r from MAC r ");

                foreach (var row in rows)
                {
                    //proxy = ReflectionExample.ToProxy<AHPproxy, AHP>(row);
                    proxy = CRUDsHelper.ToProxy<MACProxy, MAC>(row);
                    /*
                    proxy = new CTProxy
                    {
                        RowKey = row.GetObjectNo(),
                        CC = row.CC == null ? 0 : row.CC.GetObjectNo(),
                        K1 = row.K1 == null ? 0 : row.K1.GetObjectNo(),
                        K2 = row.K2 == null ? 0 : row.K1.GetObjectNo(),

                        Ad = row.Ad,
                        Adres = row.Adres ?? "",
                        Info = row.Info ?? "",
                        NG = row.NG,
                        NM = row.NM,
                        NB = row.NB,
                        NT = row.NT,
                        NX = row.NX,
                        KA = row.KA,
                        KV = row.KV,
                        KF = row.KF,
                        PW = row.PW,
                        
                        IsRun = row.IsRun,
                    };
                    */
                    proxyList.Add(proxy);
                }
            });

            foreach (var p in proxyList)
            {
                await responseStream.WriteAsync(p);
            }
        }
        public override Task<MACProxy> MACUpdate(MACProxy request, ServerCallContext context)
        {
            Scheduling.RunTask(() =>
            {
                // RowSte: Added, Modified, Deletede, Unchanged
                Db.Transact(() =>
                {
                    if (request.RowSte == "A" || request.RowSte == "M")
                    {
                        if (request.RowErr == string.Empty)
                        {
                            MAC row = CRUDsHelper.FromProxy<MACProxy, MAC>(request);
                            //XUT.Append(request.RowUsr, row, request.RowSte);
                            request = CRUDsHelper.ToProxy<MACProxy, MAC>(row);
                        }

                    }
                    else if (request.RowSte == "D")
                    {
                        var row = (MAC)Db.FromId(request.RowKey);
                        if (row == null)
                        {
                            request.RowErr = "MAC Rec not found";
                        }
                        else
                        {
                            row.Delete();
                        }
                    }
                });
            }).Wait();

            Session.RunTaskForAll((s, id) =>
            {
                s.CalculatePatchAndPushOnWebSocket();
            });

            return Task.FromResult(request);
        }

    }
}
