using RestLib;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestWinFormsClient2
{
    partial class DataSetGnl
    {
        public ulong Login(string id, string pwd)
        {
            var request = new LgnProxy
            {
                Id = id,
                Pwd = pwd,
                CcOno = 0,
            };

            var reply = grpcService.ClientCRUDs.Lgn(request);

            return reply.CcOno; // 0 degilse Bu CC ye ait CET MAClarini girebilir
        }

        public async Task<string> DDFill()
        {
            var dt = DD;
            DataRow row;
            int nor = 0;
            Stopwatch sw = new Stopwatch();
            var ct = new CancellationToken();


            dt.BeginLoadData();
            sw.Start();

            /*
             using (var call = client.ListFeatures(request))
                    {
                        var responseStream = call.ResponseStream;
                        StringBuilder responseLog = new StringBuilder("Result: ");

                        while (await responseStream.MoveNext())
            */
            var rt = grpcService.channel;

            using (var call = grpcService.ClientCRUDs.DDFill(new QryProxy { Query = "", Param = "" }))
            {

                while (await call.ResponseStream.MoveNext(CancellationToken.None))
                {
                    row = dt.NewRow();

                    ProxyHelper.ProxyToRow(dt, row, call.ResponseStream.Current);
                    dt.Rows.Add(row);

                    nor++;
                }
            }
            sw.Stop();
            dt.AcceptChanges();
            dt.EndLoadData();
            return $"{nor:n0} records retrieved in {sw.ElapsedMilliseconds:n0} ms";
        }
        public string DDUpdate()
        {
            StringBuilder sb = new StringBuilder();
            var dt = DD;
            var request = new DDProxy();
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

                    var reply = grpcService.ClientCRUDs.DDUpdate(request);  // --------->

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
