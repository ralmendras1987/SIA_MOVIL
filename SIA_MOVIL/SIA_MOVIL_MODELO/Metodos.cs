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
using GenesysJWT;
using Newtonsoft.Json;
 

namespace SIA_MOVIL_MODELO
{
    public class Metodos
    {
        public static void IniciaSesion(VM_Usuario DATA)
        {


            try
            {
                DATA.USER.USER = DATA.USER.USER.ToUpper();


                DataTable DT = new DataTable();

                OracleConnection CON = new OracleConnection(Comun._STR_CON());
                OracleCommand CMD = new OracleCommand();
                CMD.Connection = CON;
                CMD.CommandText = Comun._PACKAGE() + "SP_LOGIN";
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Add("PC_USUARIO", OracleType.VarChar, 50).Value = DATA.USER.USER;
                CMD.Parameters.Add("PC_PASS", OracleType.VarChar, 50).Value = DATA.USER.PASS;


                CMD.Parameters.Add("PC_NOMBRE", OracleType.VarChar, 50).Direction = ParameterDirection.Output;
                CMD.Parameters.Add("PN_RUT", OracleType.Number).Direction = ParameterDirection.Output;
                CMD.Parameters.Add("PN_RUT_PROVEEDOR", OracleType.Number).Direction = ParameterDirection.Output;
                CMD.Parameters.Add("PC_CORREO", OracleType.VarChar, 50).Direction = ParameterDirection.Output;



                OracleParameter PN_CODIGO_ERROR = new OracleParameter();
                PN_CODIGO_ERROR.ParameterName = "PN_CODIGO_ERROR";
                PN_CODIGO_ERROR.Direction = ParameterDirection.Output;
                PN_CODIGO_ERROR.OracleType = OracleType.Number;

                CMD.Parameters.Add(PN_CODIGO_ERROR);

                OracleParameter PC_DSC_ERROR = new OracleParameter();
                PC_DSC_ERROR.ParameterName = "PC_DSC_ERROR";
                PC_DSC_ERROR.Direction = ParameterDirection.Output;
                PC_DSC_ERROR.OracleType = OracleType.VarChar;
                PC_DSC_ERROR.Size = 2000;

                CMD.Parameters.Add(PC_DSC_ERROR);



                OracleDataAdapter DA = new OracleDataAdapter(CMD);

                CON.Open();

                CMD.ExecuteNonQuery();


                if (PC_DSC_ERROR.Value.ToString().Length == 0)
                {

                    DATA.USER.NOMBRE = CMD.Parameters["PC_NOMBRE"].Value.ToString();
                    //DATA.USER.RUT = CMD.Parameters["PN_RUT"].Value.ToString();
                    //DATA.USER.RUT_PROVEEDOR = CMD.Parameters["PN_RUT_PROVEEDOR"].Value.ToString();
                    DATA.USER.CORREO = CMD.Parameters["PC_CORREO"].Value.ToString();


                    DATA.TOKEN = GenesysJWT.JWT.GeneraToken(DATA, Comun._TIEMPO_EXPIRA_SESION());

                }
                else
                {
                    DATA.ERROR_ID = 1;
                    DATA.ERROR_DSC = PC_DSC_ERROR.Value.ToString();
                }

                CON.Close();

            }
            catch (Exception EX)
            {
                DATA.ERROR_ID = 1;
                DATA.ERROR_DSC = "Error al iniciar sesion";
                //DATA.ERROR_EX = "BaseLogin IniciaSesion: " + EX.Message;

            }

            DATA.USER.PASS = string.Empty;

        }


        //public static void GeneraMenu(Menu MENU)
        //{
        //    try
        //    {

        //        DataTable DT = new DataTable();

        //        OracleConnection CON = new OracleConnection(Comun._STR_CON());
        //        OracleCommand CMD = new OracleCommand();
        //        CMD.Connection = CON;
        //        CMD.CommandText = Comun._PACKAGE() + "SP_MENU";
        //        CMD.CommandType = CommandType.StoredProcedure;

        //        CMD.Parameters.Add("PC_USUARIO", OracleType.VarChar, 50).Value = MENU.USER.USER;


        //        CMD.Parameters.Add("P_CURSOR", OracleType.Cursor).Direction = ParameterDirection.Output;

        //        OracleDataAdapter DA = new OracleDataAdapter(CMD);



        //        DA.Fill(DT);

        //        if (DT.Rows.Count > 0)
        //        {
        //            foreach (DataRow ITEM in DT.Rows)
        //            {

        //                MenuBody ROW = new MenuBody();

        //                ROW.ID = ITEM["ID"].ToString();
        //                ROW.DSC = ITEM["DSC"].ToString();
        //                ROW.PADREID = ITEM["PADREID"].ToString();
        //                ROW.ICONO = ITEM["ICONO"].ToString();
        //                ROW.URL = ITEM["URL"].ToString();
        //                ROW.NIVEL = ITEM["NIVEL"].ToString();


        //                MENU.MENU.Add(ROW);

        //            }

        //        }



        //    }
        //    catch (Exception EX)
        //    {

        //        MENU.ERROR_ID = 1;
        //        MENU.ERROR_DSC = "Error al generar menu";
        //        MENU.ERROR_EX = "BaseLogin GeneraMenu: " + EX.Message;

        //    }


        //}

        public static bool ValidaCaptcha(string token)
        {
            if (Comun._TESTING())
            {
                return true;
            }

            return true;

            //var client = new RestClient("https://www.google.com/recaptcha/api/siteverify?secret=6LdNfqwZAAAAAMqskSRbeiS3maaulxctCtmJZx1W&response=" + token);
            //client.Timeout = -1;
            //var request = new RestRequest(Method.POST);
            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            //dynamic obj = JsonConvert.DeserializeObject<dynamic>(response.Content);


            //return obj.success;

        }


      
    }

}