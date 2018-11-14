using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestClientWinForm;
using RestLib;

namespace RestWinFormsClient
{
    partial class DataSetGnl
    {

        Stopwatch sw = new Stopwatch();

        public async Task<string> PPRDFill()
        {
            var dt = PPRD;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.PPRDFill(new QryProxy { Query = "", Param = "" }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string PPRDUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = PPRD;
            var request = new PPRDProxy();
            string rs = "";

            // Unchanged disindakileri gonder, deleted disindakileri reply ile guncelle, hata yoksa her rec icin AcceptChanges
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // States: Added, Modified, Deletede, Unchanged
                rs = dt.Rows[i].RowState.ToString().Substring(0, 1);

                if (rs == "A" || rs == "M" || rs == "D")
                {
                    dt.Rows[i].ClearErrors();
                    request.RowSte = rs;
                    //request.RowUsr = Program.ObjUsr;

                    if (rs == "D")
                        request.RowKey = (ulong)dt.Rows[i]["RowKey", DataRowVersion.Original];
                    else
                        ProxyHelper.RowToProxy(dt, dt.Rows[i], request);

                    var reply = grpcService.ClientCRUDs.PPRDUpdate(request);  // --------->

                    if (string.IsNullOrEmpty(reply.RowErr))
                    {
                        if (rs != "D")
                            ProxyHelper.ProxyToRow(dt, dt.Rows[i], reply);
                        dt.Rows[i].AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[i].RowError = reply.RowErr;
                        sb.AppendLine(reply.RowErr);
                        dt.Rows[i].RejectChanges();

                    }
                }
            }
            return sb.ToString();
        }

        public async Task<string> PPFill()
        {
            var dt = PP;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.PPFill(new QryProxy { Query = "", Param = "" }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string PPUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = PP;
            var request = new PPProxy();
            string rs = "";

            // Unchanged disindakileri gonder, deleted disindakileri reply ile guncelle, hata yoksa her rec icin AcceptChanges
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // States: Added, Modified, Deletede, Unchanged
                rs = dt.Rows[i].RowState.ToString().Substring(0, 1);

                if (rs == "A" || rs == "M" || rs == "D")
                {
                    dt.Rows[i].ClearErrors();
                    request.RowSte = rs;
                    //request.RowUsr = Program.ObjUsr;

                    if (rs == "D")
                        request.RowKey = (ulong)dt.Rows[i]["RowKey", DataRowVersion.Original];
                    else
                        ProxyHelper.RowToProxy(dt, dt.Rows[i], request);

                    var reply = grpcService.ClientCRUDs.PPUpdate(request);  // --------->

                    if (string.IsNullOrEmpty(reply.RowErr))
                    {
                        if (rs != "D")
                            ProxyHelper.ProxyToRow(dt, dt.Rows[i], reply);
                        dt.Rows[i].AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[i].RowError = reply.RowErr;
                        sb.AppendLine(reply.RowErr);
                        dt.Rows[i].RejectChanges();

                    }
                }
            }
            return sb.ToString();
        }

        public async Task<string> CCFill()
        {
            var dt = CC;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.CCFill(new QryProxy { Query = "", Param = "" }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string CCUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = CC;
            var request = new CCProxy();
            string rs = "";

            // Unchanged disindakileri gonder, deleted disindakileri reply ile guncelle, hata yoksa her rec icin AcceptChanges
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // States: Added, Modified, Deletede, Unchanged
                rs = dt.Rows[i].RowState.ToString().Substring(0, 1);

                if (rs == "A" || rs == "M" || rs == "D")
                {
                    dt.Rows[i].ClearErrors();
                    request.RowSte = rs;
                    //request.RowUsr = Program.ObjUsr;

                    if (rs == "D")
                        request.RowKey = (ulong)dt.Rows[i]["RowKey", DataRowVersion.Original];
                    else
                        ProxyHelper.RowToProxy(dt, dt.Rows[i], request);

                    var reply = grpcService.ClientCRUDs.CCUpdate(request);  // --------->

                    if (string.IsNullOrEmpty(reply.RowErr))
                    {
                        if (rs != "D")
                            ProxyHelper.ProxyToRow(dt, dt.Rows[i], reply);
                        dt.Rows[i].AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[i].RowError = reply.RowErr;
                        sb.AppendLine(reply.RowErr);
                        dt.Rows[i].RejectChanges();

                    }
                }
            }
            return sb.ToString();
        }

        public async Task<string> CTFill(string qry, ulong prm)
        {
            var dt = CT;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.CTFill(new QryProxy { Query = qry, Param = prm.ToString() }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string CTUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = CT;
            var request = new CTProxy();
            string rs = "";

            // Unchanged disindakileri gonder, deleted disindakileri reply ile guncelle, hata yoksa her rec icin AcceptChanges
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // States: Added, Modified, Deletede, Unchanged
                rs = dt.Rows[i].RowState.ToString().Substring(0, 1);

                if (rs == "A" || rs == "M" || rs == "D")
                {
                    dt.Rows[i].ClearErrors();
                    request.RowSte = rs;
                    //request.RowUsr = Program.ObjUsr;

                    if (rs == "D")
                        request.RowKey = (ulong)dt.Rows[i]["RowKey", DataRowVersion.Original];
                    else
                        ProxyHelper.RowToProxy(dt, dt.Rows[i], request);

                    var reply = grpcService.ClientCRUDs.CTUpdate(request);  // --------->

                    if (string.IsNullOrEmpty(reply.RowErr))
                    {
                        if (rs != "D")
                            ProxyHelper.ProxyToRow(dt, dt.Rows[i], reply);
                        dt.Rows[i].AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[i].RowError = reply.RowErr;
                        sb.AppendLine(reply.RowErr);
                        dt.Rows[i].RejectChanges();

                    }
                }
            }
            return sb.ToString();
        }

        public async Task<string> CTPFill(string qry, ulong prm)
        {
            var dt = CTP;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.CTPFill(new QryProxy { Query = qry, Param = prm.ToString() }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string CTPUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = CTP;
            var request = new CTPProxy();
            string rs = "";

            // Unchanged disindakileri gonder, deleted disindakileri reply ile guncelle, hata yoksa her rec icin AcceptChanges
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // States: Added, Modified, Deletede, Unchanged
                rs = dt.Rows[i].RowState.ToString().Substring(0, 1);

                if (rs == "A" || rs == "M" || rs == "D")
                {
                    dt.Rows[i].ClearErrors();
                    request.RowSte = rs;
                    //request.RowUsr = Program.ObjUsr;

                    if (rs == "D")
                        request.RowKey = (ulong)dt.Rows[i]["RowKey", DataRowVersion.Original];
                    else
                        ProxyHelper.RowToProxy(dt, dt.Rows[i], request);

                    var reply = grpcService.ClientCRUDs.CTPUpdate(request);  // --------->

                    if (string.IsNullOrEmpty(reply.RowErr))
                    {
                        if (rs != "D")
                            ProxyHelper.ProxyToRow(dt, dt.Rows[i], reply);
                        dt.Rows[i].AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[i].RowError = reply.RowErr;
                        sb.AppendLine(reply.RowErr);
                        dt.Rows[i].RejectChanges();

                    }
                }
            }
            return sb.ToString();
        }

        public async Task<string> CETFill(string qry, ulong prm)
        {
            var dt = CET;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.CETFill(new QryProxy { Query = qry, Param = prm.ToString() }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string CETUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = CET;
            var request = new CETProxy();
            string rs = "";

            // Unchanged disindakileri gonder, deleted disindakileri reply ile guncelle, hata yoksa her rec icin AcceptChanges
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // States: Added, Modified, Deletede, Unchanged
                rs = dt.Rows[i].RowState.ToString().Substring(0, 1);

                if (rs == "A" || rs == "M" || rs == "D")
                {
                    dt.Rows[i].ClearErrors();
                    request.RowSte = rs;
                    //request.RowUsr = Program.ObjUsr;

                    if (rs == "D")
                        request.RowKey = (ulong)dt.Rows[i]["RowKey", DataRowVersion.Original];
                    else
                        ProxyHelper.RowToProxy(dt, dt.Rows[i], request);

                    var reply = grpcService.ClientCRUDs.CETUpdate(request);  // --------->

                    if (string.IsNullOrEmpty(reply.RowErr))
                    {
                        if (rs != "D")
                            ProxyHelper.ProxyToRow(dt, dt.Rows[i], reply);
                        dt.Rows[i].AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[i].RowError = reply.RowErr;
                        sb.AppendLine(reply.RowErr);
                        dt.Rows[i].RejectChanges();

                    }
                }
            }
            return sb.ToString();
        }

        public async Task<string> CFFill(string qry, ulong prm)
        {
            var dt = CF;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.CFFill(new QryProxy { Query = qry, Param = prm.ToString() }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string CFUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = CF;
            var request = new CFProxy();
            string rs = "";

            // Unchanged disindakileri gonder, deleted disindakileri reply ile guncelle, hata yoksa her rec icin AcceptChanges
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // States: Added, Modified, Deletede, Unchanged
                rs = dt.Rows[i].RowState.ToString().Substring(0, 1);

                if (rs == "A" || rs == "M" || rs == "D")
                {
                    dt.Rows[i].ClearErrors();
                    request.RowSte = rs;
                    //request.RowUsr = Program.ObjUsr;

                    if (rs == "D")
                        request.RowKey = (ulong)dt.Rows[i]["RowKey", DataRowVersion.Original];
                    else
                        ProxyHelper.RowToProxy(dt, dt.Rows[i], request);

                    var reply = grpcService.ClientCRUDs.CFUpdate(request);  // --------->

                    if (string.IsNullOrEmpty(reply.RowErr))
                    {
                        if (rs != "D")
                            ProxyHelper.ProxyToRow(dt, dt.Rows[i], reply);
                        dt.Rows[i].AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[i].RowError = reply.RowErr;
                        sb.AppendLine(reply.RowErr);
                        dt.Rows[i].RejectChanges();

                    }
                }
            }
            return sb.ToString();
        }

        public async Task<string> CEFFill(string qry, ulong prm)
        {
            var dt = CEF;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.CEFFill(new QryProxy { Query = qry, Param = prm.ToString() }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string CEFUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = CEF;
            var request = new CEFProxy();
            string rs = "";

            // Unchanged disindakileri gonder, deleted disindakileri reply ile guncelle, hata yoksa her rec icin AcceptChanges
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // States: Added, Modified, Deletede, Unchanged
                rs = dt.Rows[i].RowState.ToString().Substring(0, 1);

                if (rs == "A" || rs == "M" || rs == "D")
                {
                    dt.Rows[i].ClearErrors();
                    request.RowSte = rs;
                    //request.RowUsr = Program.ObjUsr;

                    if (rs == "D")
                        request.RowKey = (ulong)dt.Rows[i]["RowKey", DataRowVersion.Original];
                    else
                        ProxyHelper.RowToProxy(dt, dt.Rows[i], request);

                    var reply = grpcService.ClientCRUDs.CEFUpdate(request);  // --------->

                    if (string.IsNullOrEmpty(reply.RowErr))
                    {
                        if (rs != "D")
                            ProxyHelper.ProxyToRow(dt, dt.Rows[i], reply);
                        dt.Rows[i].AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[i].RowError = reply.RowErr;
                        sb.AppendLine(reply.RowErr);
                        dt.Rows[i].RejectChanges();

                    }
                }
            }
            return sb.ToString();
        }

        public async Task<string> MACFill(string qry, ulong prm)
        {
            var dt = MAC;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.MACFill(new QryProxy { Query = qry, Param = prm.ToString() }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string MACUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = MAC;
            var request = new MACProxy();
            string rs = "";

            // Unchanged disindakileri gonder, deleted disindakileri reply ile guncelle, hata yoksa her rec icin AcceptChanges
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                // States: Added, Modified, Deletede, Unchanged
                rs = dt.Rows[i].RowState.ToString().Substring(0, 1);

                if (rs == "A" || rs == "M" || rs == "D")
                {
                    dt.Rows[i].ClearErrors();
                    request.RowSte = rs;
                    //request.RowUsr = Program.ObjUsr;

                    if (rs == "D")
                        request.RowKey = (ulong)dt.Rows[i]["RowKey", DataRowVersion.Original];
                    else
                        ProxyHelper.RowToProxy(dt, dt.Rows[i], request);

                    var reply = grpcService.ClientCRUDs.MACUpdate(request);  // --------->

                    if (string.IsNullOrEmpty(reply.RowErr))
                    {
                        if (rs != "D")
                            ProxyHelper.ProxyToRow(dt, dt.Rows[i], reply);
                        dt.Rows[i].AcceptChanges();
                    }
                    else
                    {
                        dt.Rows[i].RowError = reply.RowErr;
                        sb.AppendLine(reply.RowErr);
                        dt.Rows[i].RejectChanges();

                    }
                }
            }
            return sb.ToString();
        }

        public async Task<string> PPlookUp()
        {
            var dt = PPlu;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();

            dt.BeginLoadData();
            sw.Start();
            using (var response = grpcService.ClientCRUDs.PPlookUp(new QryProxy { Query = "", Param = "" }))
            {
                while (await response.ResponseStream.MoveNext(new CancellationToken()))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, response.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }


        public string PerfomAction(string action, string prm1 = "", string prm2 = "")
        {
            var request = new ActionProxy
            {
                Req = action,
                Prm1 = prm1,
                Prm2 = prm2,
                Rsp = ""
            };

            var reply = grpcService.ClientCRUDs.PerformAction(request);  // --------->

            return reply.Rsp;
        }
    }
}
