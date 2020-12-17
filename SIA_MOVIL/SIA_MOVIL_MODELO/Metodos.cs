using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
 

namespace SIA_MOVIL_MODELO
{
    public class Metodos
    {

        public static void login(Historico DATA)
        {
            string ex = string.Empty;
            try
            {
                DATA.LOGIN.usuario = Comun._USUARIO();
                DATA.LOGIN.password = Comun._PASS();

                var client = new RestClient(Comun._APIURLLOGIN());
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(DATA.LOGIN), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                DATA.TOKEN = JsonConvert.DeserializeObject<Token>(response.Content);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ex = (response.ErrorMessage == null) ? "Error: " + response.StatusCode : response.ErrorMessage;
                    DATA.ERRORES.Add("login: " + ex.ToString());
                }
                else
                {
                    if (DATA.TOKEN.status.ToUpper() == "ERROR")
                    {
                        DATA.ERRORES.Add("login: " + DATA.TOKEN.value + ": " + ex.ToString());
                    }
                }

            }
            catch (Exception e)
            {
                DATA.ERRORES.Add("login: " + ex.ToString() + ": " + e.Message);
            }


        }

        public static void getHistorico(Historico DATA)
        {

            DATA.resultado.Clear();

            try
            {

                foreach (Consulta ITEM in DATA.consulta)
                {

                    var request = WebRequest.Create(new Uri(Comun._APIURLHISTORICO()));

                    request.ContentType = "application/json";
                    request.Headers.Add("Authorization", "Bearer " + DATA.TOKEN.value);
                    request.Method = "GET";

                    var type = request.GetType();
                    var currentMethod = type.GetProperty("CurrentMethod", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(request);

                    var methodType = currentMethod.GetType();
                    methodType.GetField("ContentBodyNotAllowed", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(currentMethod, false);

                    List<object> consulta = new List<object>();


                    consulta.Add(new
                    {
                        dispositivoId = ITEM.dispositivoId
                    });

                    string json = JsonConvert.SerializeObject(new
                    {
                        estampaTiempoInicial = ITEM.estampaTiempoInicial,
                        estampaTiempoFinal = ITEM.estampaTiempoFinal,
                        tipoMedicion = ITEM.tipoMedicion,
                        consulta = consulta
                    });

                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(json);
                    }

                    using (Stream s = request.GetResponse().GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                            Historico R = new Historico();
                            R = JsonConvert.DeserializeObject<Historico>(jsonResponse);

                            DATA.codigo = R.codigo;

                            if (R.resultado != null)
                            {

                                foreach (Resultado RES in R.resultado)
                                {

                                    foreach (Parametros ROW in RES.parametros)
                                    {
                                        ROW.estacionid = ITEM.estacionid;
                                        ROW.plantaid = ITEM.plantaid;
                                        ROW.estacionActiva = ITEM.estacionActiva;

                                    }

                                    DATA.resultado.Add(RES);
                                }

                            }

                        }
                    }
                }




            }
            catch (Exception e)
            {
                DATA.ERRORES.Add("getHistorico: " + e.Message);

            }


        }

        public static void SP_LISTA_ESTACIONES_1H(Historico DATA)
        {

            try
            {
                DATA.consulta = new List<Consulta>();

                DataTable DT = new DataTable();

                OracleConnection CON = new OracleConnection(Comun._STR_CON());
                OracleCommand CMD = new OracleCommand();
                CMD.Connection = CON;
                CMD.CommandText = Comun._PACKAGE() + "SP_LISTA_ESTACIONES_1H";
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Add("VV_PLANTA", OracleType.VarChar, DATA.filtros.PLANTA.Length).Value = DATA.filtros.PLANTA;
                CMD.Parameters.Add("VN_ESTACION", OracleType.Number).Value = DATA.filtros.ESTACION;
                CMD.Parameters.Add("VD_FECHA_INI", OracleType.DateTime).Value = DATA.filtros.FECHA_INI;
                CMD.Parameters.Add("VD_FECHA_FIN", OracleType.DateTime).Value = DATA.filtros.FECHA_FIN;

                CMD.Parameters.Add("IO_CURSOR", OracleType.Cursor).Direction = ParameterDirection.Output;


                OracleDataAdapter DA = new OracleDataAdapter(CMD);

                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow ITEM in DT.Rows)
                    {

                        try
                        {

                            DATA.consulta.Add(new Consulta
                            {
                                dispositivoId = ITEM["dispositivoId"].ToString(),
                                plantaid = ITEM["plantaid"].ToString(),
                                estacionid = ITEM["estacionid"].ToString(),
                                tipoMedicion = ITEM["tipoMedicion"].ToString(),
                                estacionActiva = ITEM["estacionActiva"].ToString(),
                                estampaTiempoInicial = ITEM["estampaTiempoInicial"].ToString(),
                                estampaTiempoFinal = ITEM["estampaTiempoFinal"].ToString()
                            });

                        }
                        catch (Exception EX)
                        {

                            DATA.ERRORES.Add("SP_LISTA_ESTACIONES_1H: Dato invalido dentro de registro Consulta" + EX.Message);

                        }


                    }

                }
                else
                {
                    DATA.ERRORES.Add("SP_LISTA_ESTACIONES_1H: Lista vacia");
                }

            }
            catch (Exception EX)
            {

                DATA.ERRORES.Add("SP_LISTA_ESTACIONES_1H: " + EX.Message);

            }

        }

        public static void SP_LISTA_ESTACIONES_1M(Historico DATA)
        {

            try
            {
                DATA.consulta = new List<Consulta>();

                DataTable DT = new DataTable();

                OracleConnection CON = new OracleConnection(Comun._STR_CON());
                OracleCommand CMD = new OracleCommand();
                CMD.Connection = CON;
                CMD.CommandText = Comun._PACKAGE() + "SP_LISTA_ESTACIONES_1M";
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Add("VV_PLANTA", OracleType.VarChar, DATA.filtros.PLANTA.Length).Value = DATA.filtros.PLANTA;
                CMD.Parameters.Add("VN_ESTACION", OracleType.Number).Value = DATA.filtros.ESTACION;
                CMD.Parameters.Add("VD_FECHA_INI", OracleType.DateTime).Value = DATA.filtros.FECHA_INI;
                CMD.Parameters.Add("VD_FECHA_FIN", OracleType.DateTime).Value = DATA.filtros.FECHA_FIN;

                CMD.Parameters.Add("IO_CURSOR", OracleType.Cursor).Direction = ParameterDirection.Output;


                OracleDataAdapter DA = new OracleDataAdapter(CMD);

                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow ITEM in DT.Rows)
                    {

                        try
                        {

                            DATA.consulta.Add(new Consulta
                            {
                                dispositivoId = ITEM["dispositivoId"].ToString(),
                                plantaid = ITEM["plantaid"].ToString(),
                                estacionid = ITEM["estacionid"].ToString(),
                                tipoMedicion = ITEM["tipoMedicion"].ToString(),
                                estacionActiva = ITEM["estacionActiva"].ToString(),
                                estampaTiempoInicial = ITEM["estampaTiempoInicial"].ToString(),
                                estampaTiempoFinal = ITEM["estampaTiempoFinal"].ToString()
                            });

                        }
                        catch (Exception EX)
                        {

                            DATA.ERRORES.Add("SP_LISTA_ESTACIONES_1M: Dato invalido dentro de registro Consulta" + EX.Message);

                        }


                    }

                }
                else
                {
                    DATA.ERRORES.Add("SP_LISTA_ESTACIONES_1M: Lista vacia");
                }

            }
            catch (Exception EX)
            {

                DATA.ERRORES.Add("SP_LISTA_ESTACIONES_1M: " + EX.Message);

            }


        }


        public static void CARGA_VALORES_API_1M(Historico DATA)
        {
            OracleConnection CON = new OracleConnection(Comun._STR_CON());
            try
            {

                DataTable DT = new DataTable();
                OracleCommand CMD = new OracleCommand();
                CON.Open();
                foreach (Resultado ITEM in DATA.resultado)
                {

                    foreach (Parametros ROW in ITEM.parametros)
                    {
                        try
                        {
                            DT = new DataTable();
                            CMD = new OracleCommand();

                            CMD.Connection = CON;
                            CMD.CommandText = Comun._PACKAGE() + "CARGA_VALORES_API_1M";
                            CMD.CommandType = CommandType.StoredProcedure;

                            CMD.Parameters.Add("VV_PLANTA", OracleType.VarChar, ROW.plantaid.Length).Value = ROW.plantaid;
                            CMD.Parameters.Add("VN_ESTACION", OracleType.Number).Value = ROW.estacionid;
                            CMD.Parameters.Add("VV_ESTACION_ACTIVA", OracleType.VarChar, ROW.estacionActiva.Length).Value = ROW.estacionActiva;
                            CMD.Parameters.Add("VV_NOMBRE", OracleType.VarChar, ROW.nombre.Length).Value = ROW.nombre;
                            CMD.Parameters.Add("VN_VALOR", OracleType.Number).Value = ROW.valor.Replace(".", ",");
                            CMD.Parameters.Add("VV_UNIDAD", OracleType.VarChar, ROW.unidad.Length).Value = ROW.unidad;
                            CMD.Parameters.Add("VV_ESTAMPATIEMPO", OracleType.VarChar, ROW.estampaTiempo.Length).Value = ROW.estampaTiempo;
                            CMD.Parameters.Add("VV_ESTADO", OracleType.VarChar, 500).Value = (ROW.estado == null) ? "" : ROW.estado;

                            CMD.ExecuteNonQuery();
                        }
                        catch (Exception EX)
                        {
                            DATA.ERRORES.Add("CARGA_VALORES_API_1M:" + ROW.plantaid + " - " + ROW.estacionid + " - " + ROW.nombre + " - " + ROW.estampaTiempo + " - " + ROW.valor.Replace(".", ",") + " - " + EX.Message);

                        }

                    }

                }
                CON.Close();
                CON.Dispose();

            }
            catch (Exception EX)
            {
                CON.Close();
                CON.Dispose();
                DATA.ERRORES.Add("CARGA_VALORES_API_1M:" + EX.Message);

            }
            CON.Close();
            CON.Dispose();
        }

        public static void CARGA_VALORES_API_1H(Historico DATA)
        {
            OracleConnection CON = new OracleConnection(Comun._STR_CON());
            try
            {

                DataTable DT = new DataTable();
                OracleCommand CMD = new OracleCommand();

                CON.Open();

                foreach (Resultado ITEM in DATA.resultado)
                {

                    foreach (Parametros ROW in ITEM.parametros)
                    {
                        try
                        {
                            DT = new DataTable();
                            CMD = new OracleCommand();

                            CMD.Connection = CON;
                            CMD.CommandText = Comun._PACKAGE() + "CARGA_VALORES_API_1H";
                            CMD.CommandType = CommandType.StoredProcedure;

                            CMD.Parameters.Add("VV_PLANTA", OracleType.VarChar, ROW.plantaid.Length).Value = ROW.plantaid;
                            CMD.Parameters.Add("VN_ESTACION", OracleType.Number).Value = ROW.estacionid;
                            CMD.Parameters.Add("VV_ESTACION_ACTIVA", OracleType.VarChar, ROW.estacionActiva.Length).Value = ROW.estacionActiva;
                            CMD.Parameters.Add("VV_NOMBRE", OracleType.VarChar, ROW.nombre.Length).Value = ROW.nombre;
                            CMD.Parameters.Add("VN_VALOR", OracleType.Number).Value = ROW.valor.Replace(".", ",");
                            CMD.Parameters.Add("VV_UNIDAD", OracleType.VarChar, ROW.unidad.Length).Value = ROW.unidad;
                            CMD.Parameters.Add("VV_ESTAMPATIEMPO", OracleType.VarChar, ROW.estampaTiempo.Length).Value = ROW.estampaTiempo;
                            CMD.Parameters.Add("VV_ESTADO", OracleType.VarChar, 500).Value = (ROW.estado == null) ? "" : ROW.estado;

                            CMD.ExecuteNonQuery();
                        }
                        catch (Exception EX)
                        {
                            DATA.ERRORES.Add("CARGA_VALORES_API_1H:" + ROW.plantaid + " - " + ROW.estacionid + " - " + ROW.nombre + " - " + ROW.estampaTiempo + " - " + ROW.valor.Replace(".", ",") + " - " + EX.Message);

                        }

                    }

                }

                CON.Close();
            }
            catch (Exception EX)
            {
                CON.Close();
                CON.Dispose();
                DATA.ERRORES.Add("CARGA_VALORES_API_1H:" + EX.Message);

            }

            CON.Close();
            CON.Dispose();
        }


        public static void CARGA_LOG_ERROR(List<string> DATA, string TITULO, string PARAMETROS)
        {
            OracleConnection CON = new OracleConnection(Comun._STR_CON());

            foreach (string ITEM in DATA)
            {
                try
                {
                    DataTable DT = new DataTable();
                    OracleCommand CMD = new OracleCommand();

                    DT = new DataTable();
                    CMD = new OracleCommand();

                    CMD.Connection = CON;
                    CMD.CommandText = Comun._PACKAGE() + "CARGA_LOG_ERROR";
                    CMD.CommandType = CommandType.StoredProcedure;

                    CMD.Parameters.Add("VV_NOMBRE_PROCESO", OracleType.VarChar, TITULO.Length).Value = TITULO;
                    CMD.Parameters.Add("VN_NUMERO_ERROR", OracleType.VarChar, 100).Value = DBNull.Value;
                    CMD.Parameters.Add("VV_DESC_ERROR", OracleType.VarChar, ITEM.Length).Value = ITEM;
                    CMD.Parameters.Add("VV_PARAMETROS", OracleType.VarChar, PARAMETROS.Length).Value = PARAMETROS;
                    CON.Open();
                    CMD.ExecuteNonQuery();
                    CON.Close();
                }
                catch (Exception EX)
                {

                    CON.Close();

                }
                CON.Close();

            }


        }

    }


}