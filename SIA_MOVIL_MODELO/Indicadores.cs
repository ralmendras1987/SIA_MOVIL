using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA_MOVIL_MODELO
{
    public class VM_Indicadores
    {
        public string VN_FILIAL = string.Empty;
        public string VV_PLANTA = string.Empty;
        public string VV_FECHAINICIO = string.Empty;
        public string VV_FECHAFIN = string.Empty;
        public string VV_USUARIO = string.Empty;

        public List<Indicadores> GRILLA = new List<Indicadores>();


        public VM_Usuario SESSION = new VM_Usuario();
        public int ERROR_ID = 0;
        public string ERROR_DSC = string.Empty;
        public string ERROR_EX = string.Empty;
    }

    public class VM_Indicadores_Det
    {
        public string VN_FILIAL = string.Empty;
        public string VV_PLANTA = string.Empty;
        public string VV_FECHAINICIO = string.Empty;
        public string VV_TIPO_FECHA = string.Empty;
        public string VN_TIPO_INDICADOR = string.Empty;
        public string VV_USUARIO = string.Empty;

        public List<Indicadores_Det> DETALLE = new List<Indicadores_Det>();


        public VM_Usuario SESSION = new VM_Usuario();
        public int ERROR_ID = 0;
        public string ERROR_DSC = string.Empty;
        public string ERROR_EX = string.Empty;
    }

    public class Indicadores
    {
        public string FILIAL_ID = string.Empty;
        public string FILIAL_DSC = string.Empty;
        public string PLANTA_ID = string.Empty;
        public string PLANTA_DSC = string.Empty;
        public string RECLAMOS_MES = string.Empty;
        public string RECLAMOS_AGNO = string.Empty;
        public string RECLAMO_TIPO = string.Empty;
        public string INCIDENTE_MES = string.Empty;
        public string INCIDENTE_AGNO = string.Empty;
        public string INCIDENTE_TIPO = string.Empty;
        public string FISCALIZACION_MES = string.Empty;
        public string FISCALIZACION_AGNO = string.Empty;
        public string FISCALIZACION_TIPO = string.Empty;
    }

    public class Indicadores_Det
    {
        public string FECHA = string.Empty;
        public string PLANTA_DSC = string.Empty;
        public string ORGANISMO = string.Empty;
        public string TIPO = string.Empty;
        public string DESCRIPCION = string.Empty;

    }

    public class Indicadores_Metodos
    {

        public static void SP_LISTA_INDICADORES(VM_Indicadores DATA)
        {
            try
            {

                DataTable DT = new DataTable();

                OracleConnection CON = new OracleConnection(Comun._STR_CON());
                OracleCommand CMD = new OracleCommand();
                CMD.Connection = CON;
                CMD.CommandText = Comun._PACKAGE() + "SP_LISTA_INDICADORES";
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Add("VN_FILIAL", OracleType.Number).Value = Comun.VALIDA_NULO(DATA.VN_FILIAL);
                CMD.Parameters.Add("VV_PLANTA", OracleType.VarChar, 50).Value = Comun.VALIDA_NULO(DATA.VV_PLANTA);
                CMD.Parameters.Add("VV_FECHAINICIO", OracleType.VarChar, 50).Value = Comun.VALIDA_NULO(DATA.VV_FECHAINICIO);
                CMD.Parameters.Add("VV_FECHAFIN", OracleType.VarChar, 50).Value = Comun.VALIDA_NULO(DATA.VV_FECHAFIN);
                CMD.Parameters.Add("VV_USUARIO", OracleType.VarChar, 50).Value = Comun.VALIDA_NULO(DATA.VV_USUARIO);


                CMD.Parameters.Add("IO_CURSOR", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter DA = new OracleDataAdapter(CMD);



                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow ITEM in DT.Rows)
                    {

                        Indicadores ROW = new Indicadores();

                        ROW.FILIAL_ID = ITEM["FILIAL_ID"].ToString();
                        ROW.FILIAL_DSC = ITEM["FILIAL_DSC"].ToString();
                        ROW.PLANTA_ID = ITEM["PLANTA_ID"].ToString();
                        ROW.PLANTA_DSC = ITEM["PLANTA_DSC"].ToString();
                        ROW.RECLAMOS_MES = ITEM["RECLAMOS_MES"].ToString();
                        ROW.RECLAMOS_AGNO = ITEM["RECLAMOS_AGNO"].ToString();
                        ROW.RECLAMO_TIPO = ITEM["RECLAMO_TIPO"].ToString();
                        ROW.INCIDENTE_MES = ITEM["INCIDENTE_MES"].ToString();
                        ROW.INCIDENTE_AGNO = ITEM["INCIDENTE_AGNO"].ToString();
                        ROW.INCIDENTE_TIPO = ITEM["INCIDENTE_TIPO"].ToString();
                        ROW.FISCALIZACION_MES = ITEM["FISCALIZACION_MES"].ToString();
                        ROW.FISCALIZACION_AGNO = ITEM["FISCALIZACION_AGNO"].ToString();
                        ROW.FISCALIZACION_TIPO = ITEM["FISCALIZACION_TIPO"].ToString();



                        DATA.GRILLA.Add(ROW);

                    }

                }



            }
            catch (Exception EX)
            {

                DATA.ERROR_ID = 1;
                DATA.ERROR_DSC = "Error al generar grilla";
                DATA.ERROR_EX = "Indicadores SP_LISTA_INDICADORES: " + EX.Message;

            }


        }
        public static void SP_LISTA_INDICADORES_DET(VM_Indicadores_Det DATA)
        {
            try
            {

                DataTable DT = new DataTable();

                OracleConnection CON = new OracleConnection(Comun._STR_CON());
                OracleCommand CMD = new OracleCommand();
                CMD.Connection = CON;
                CMD.CommandText = Comun._PACKAGE() + "SP_LISTA_INDICADORES_DET";
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Add("VN_FILIAL", OracleType.Number).Value = Comun.VALIDA_NULO(DATA.VN_FILIAL);
                CMD.Parameters.Add("VV_PLANTA", OracleType.VarChar, 50).Value = Comun.VALIDA_NULO(DATA.VV_PLANTA);
                CMD.Parameters.Add("VV_FECHAINICIO", OracleType.VarChar, 50).Value = Comun.VALIDA_NULO(DATA.VV_FECHAINICIO);
                CMD.Parameters.Add("VV_TIPO_FECHA", OracleType.VarChar, 50).Value = Comun.VALIDA_NULO(DATA.VV_TIPO_FECHA);
                CMD.Parameters.Add("VN_TIPO_INDICADOR", OracleType.Number).Value = Comun.VALIDA_NULO(DATA.VN_TIPO_INDICADOR);
                CMD.Parameters.Add("VV_USUARIO", OracleType.VarChar, 50).Value = Comun.VALIDA_NULO(DATA.VV_USUARIO);


                CMD.Parameters.Add("IO_CURSOR", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter DA = new OracleDataAdapter(CMD);



                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow ITEM in DT.Rows)
                    {

                        Indicadores_Det ROW = new Indicadores_Det();

                        ROW.FECHA = ITEM["FECHA"].ToString();
                        ROW.PLANTA_DSC = ITEM["PLANTA_DSC"].ToString();
                        ROW.ORGANISMO = ITEM["ORGANISMO"].ToString();
                        ROW.TIPO = ITEM["TIPO"].ToString();
                        ROW.DESCRIPCION = ITEM["DESCRIPCION"].ToString();




                        DATA.DETALLE.Add(ROW);

                    }

                }



            }
            catch (Exception EX)
            {

                DATA.ERROR_ID = 1;
                DATA.ERROR_DSC = "Error al generar grilla";
                DATA.ERROR_EX = "Indicadores SP_LISTA_INDICADORES: " + EX.Message;

            }


        }

    }
}
