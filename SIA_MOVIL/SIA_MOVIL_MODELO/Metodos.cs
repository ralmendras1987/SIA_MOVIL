using System;
using System.Data;
using System.Data.OracleClient;


namespace SIA_MOVIL_MODELO
{
    public class Metodos
    {
        public static void IniciaSesion(VM_Usuario DATA)
        {


            try
            {
                DATA.USER_DATA.USER = DATA.USER_DATA.USER.ToUpper();


                DataTable DT = new DataTable();

                OracleConnection CON = new OracleConnection(Comun._STR_CON());
                OracleCommand CMD = new OracleCommand();
                CMD.Connection = CON;
                CMD.CommandText = Comun._PACKAGE() + "SP_VALIDA_USUARIO";
                CMD.CommandType = CommandType.StoredProcedure;

                CMD.Parameters.Add("VV_USUARIO", OracleType.VarChar, 50).Value = DATA.USER_DATA.USER;
                CMD.Parameters.Add("VV_PASSWORD", OracleType.VarChar, 50).Value = DATA.USER_DATA.PASS;


                OracleParameter PC_DSC_ERROR = new OracleParameter();
                PC_DSC_ERROR.ParameterName = "VV_MENSAJE";
                PC_DSC_ERROR.Direction = ParameterDirection.Output;
                PC_DSC_ERROR.OracleType = OracleType.VarChar;
                PC_DSC_ERROR.Size = 2000;

                CMD.Parameters.Add(PC_DSC_ERROR);

                CMD.Parameters.Add("IO_CURSOR", OracleType.Cursor).Direction = ParameterDirection.Output;

                OracleDataAdapter DA = new OracleDataAdapter(CMD);

                DA.Fill(DT);

                if (DT.Rows.Count > 0)
                {
                    foreach (DataRow ITEM in DT.Rows)
                    {

                        DATA.USER_DATA.NOMBRE = ITEM["USUA_NOMBRE"].ToString();
                        DATA.USER_DATA.CORREO = ITEM["USUA_EMAIL"].ToString();
                        DATA.USER_DATA.TELEFONO = ITEM["USUA_TELEFONO"].ToString();

                    }

                    DATA.TOKEN = GenesysJWT.JWT.GeneraToken(DATA);

                }
                else
                {
                    DATA.ERROR_ID = 1;
                    DATA.ERROR_DSC = "Usuario no registrado";
                }




            }
            catch (Exception EX)
            {
                DATA.ERROR_ID = 1;
                DATA.ERROR_DSC = "Error al iniciar sesion";
                //DATA.ERROR_EX = "BaseLogin IniciaSesion: " + EX.Message;

            }

            DATA.USER_DATA.PASS = string.Empty;

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