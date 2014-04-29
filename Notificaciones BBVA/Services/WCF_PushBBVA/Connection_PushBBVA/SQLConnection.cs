using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace Connection_PushBBVA
{
    public class SQLConnection
    {

        public static string conex = "Data Source=RODOLFOCORT6393;Initial Catalog=BBVA;Persist Security Info=True;User ID=sa;Password=q1w2e3";
        

        public static bool Login(string user, string pass)
        {
           
            string sql = @"SELECT COUNT(*)
                       FROM Admin
                       WHERE idAdmin = @usuario AND password = @password ";

            using (SqlConnection conn = new SqlConnection(connectionString: conex))
            {
              
                    conn.Open();

                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@usuario", user);
                    command.Parameters.AddWithValue("@password", pass);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 0)
                        return false;
                    else
                        return true;

            }
        }

        public static Data_PushBBVA.Login Login2(string user, string pass)
        {

            string sql = @"SELECT *
                       FROM Admin
                       WHERE idAdmin = @usuario AND password = @password ";


            using (SqlConnection conn = new SqlConnection(connectionString: conex))
            {
                Data_PushBBVA.Login access = new Data_PushBBVA.Login();

                try
                {
                    SqlCommand command = new SqlCommand(sql, conn);

                    command.Parameters.AddWithValue("@usuario", user);
                    command.Parameters.AddWithValue("@password", pass);

                    conn.Open();

                    SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
                    DataSet dtDatos = new DataSet();
                    daAdaptador.Fill(dtDatos);

                    foreach (DataRow _dr in dtDatos.Tables[0].Rows)
                    {
                        access.name = _dr["firtsName"].ToString() + " " + _dr["lastName"].ToString();
                        access.user = _dr["idAdmin"].ToString();
                        access.status = "Success";
                    }

                    return access;

                }
                catch (Exception ex)
                {
                    access.status = "Error";
                    return access;
                }

            }
        }

        public static List<Data_PushBBVA.NotificationType> NotificationType() 
        {

            List<Data_PushBBVA.NotificationType> Type = new List<Data_PushBBVA.NotificationType>();

            string sql = @"SELECT * FROM NotificationType";

            using (SqlConnection conn = new SqlConnection(connectionString: conex))
            {             
                try
                {
                    SqlCommand command = new SqlCommand(sql, conn);                   

                    conn.Open();

                    SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
                    DataSet dtDatos = new DataSet();
                    daAdaptador.Fill(dtDatos);

                    foreach (DataRow _dr in dtDatos.Tables[0].Rows)
                    {
                        Data_PushBBVA.NotificationType types = new Data_PushBBVA.NotificationType();
                        types.idNotificationType = int.Parse(_dr["idNotificationType"].ToString());
                        types.description = _dr["description"].ToString();
                        types.text = _dr["text"].ToString();
                        Type.Add(types);
                    }

                    return Type;

                }
                catch (Exception ex)
                {
                   
                    return Type;
                }

            }

            
        }

        public static string createUser(string user, string idDevice)
        {
            string status = "";
            string IdUser = "";
            string IdDevice = "";
            string token = "3290a71fec3cbb5baaf13dda7b465b82d7f4c552e9a8f69daf9f2679afb6b74d";
            string plataforma = "1";
            string firstName = "Alexis";
            string lastName = "Diaz";
            string appVersion = "1.0";
            string appId = "1.0";

            string sql = @"SELECT COUNT(*) FROM Users WHERE rut = @usuario";

            using (SqlConnection conn = new SqlConnection(connectionString: conex))
            {
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue("@usuario", user);
                    int count = 0;
                    conn.Open();
                    count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 0)
                    {                        
                            string createUser = @"INSERT INTO Users (firstName, lastName, rut, creation, status)
                                                 VALUES (@firstName, @lastName, @rut, @creation, @status)";

                            using (SqlConnection conn2 = new SqlConnection(connectionString: conex))
                            {
                                SqlCommand command2 = new SqlCommand(createUser, conn2);
                                command2.Parameters.AddWithValue("@firstName", firstName);
                                command2.Parameters.AddWithValue("@lastName", lastName);
                                command2.Parameters.AddWithValue("@rut", user);
                                command2.Parameters.AddWithValue("@creation", DateTime.Now);
                                command2.Parameters.AddWithValue("@status", 1);
                                conn2.Open();
                                command2.ExecuteScalar();
                            }

                            string selectUser = @"SELECT idUser FROM Users WHERE rut = @rut";                          
                          
                            using (SqlConnection conn3 = new SqlConnection(connectionString: conex))
                            {
                                SqlCommand command3 = new SqlCommand(selectUser, conn3);
                                command3.Parameters.AddWithValue("@rut", user);
                                conn3.Open();
                            
                                SqlDataAdapter daAdaptador = new SqlDataAdapter(command3);
                                DataSet dtDatos = new DataSet();
                                daAdaptador.Fill(dtDatos);

                                foreach (DataRow _dr in dtDatos.Tables[0].Rows)
                                {
                                    IdUser = _dr["idUser"].ToString();
                                }                              
                            }

                            string createDevice = @"INSERT INTO Device (token, appId, appVersion, creation, status, idPlataform)
                                                 VALUES (@token, @appId, @appVersion, @creation, @status, @idPlataform)";

                            using (SqlConnection conn4 = new SqlConnection(connectionString: conex))
                            {
                                SqlCommand command4 = new SqlCommand(createDevice, conn4);
                                command4.Parameters.AddWithValue("@token", token);
                                command4.Parameters.AddWithValue("@appId", appId);
                                command4.Parameters.AddWithValue("@appVersion", appVersion);
                                command4.Parameters.AddWithValue("@creation", DateTime.Now);
                                command4.Parameters.AddWithValue("@status", 1);
                                command4.Parameters.AddWithValue("@idPlataform", plataforma);
                                conn4.Open();
                                command4.ExecuteScalar();
                            }

                            string selectDevice = @"SELECT idDevice FROM Device WHERE token = @token";

                            using (SqlConnection conn5 = new SqlConnection(connectionString: conex))
                            {
                                SqlCommand command5 = new SqlCommand(selectDevice, conn5);
                                command5.Parameters.AddWithValue("@token", token);
                                conn5.Open();

                                SqlDataAdapter daAdaptador = new SqlDataAdapter(command5);
                                DataSet dtDatos = new DataSet();
                                daAdaptador.Fill(dtDatos);

                                foreach (DataRow _dr in dtDatos.Tables[0].Rows)
                                {
                                    IdDevice = _dr["idDevice"].ToString();
                                }
                            }

                            string create = @"INSERT INTO User_Device (idUser, idDevice)
                                                 VALUES (@idUser, @idDevice)";

                            using (SqlConnection conn6 = new SqlConnection(connectionString: conex))
                            {
                                SqlCommand command6 = new SqlCommand(create, conn6);
                                command6.Parameters.AddWithValue("@idUser", IdUser);
                                command6.Parameters.AddWithValue("@idDevice", IdDevice);                
                                conn6.Open();
                                command6.ExecuteScalar();
                            }
                            
                            string selectNotification = @"SELECT idNotificationType FROM NotificationType";

                            using (SqlConnection conn7 = new SqlConnection(connectionString: conex))
                            {
                                SqlCommand command7 = new SqlCommand(selectNotification, conn7);
                                conn7.Open();

                                SqlDataAdapter daAdaptador = new SqlDataAdapter(command7);
                                DataSet dtDatos = new DataSet();
                                daAdaptador.Fill(dtDatos);

                                foreach (DataRow _dr in dtDatos.Tables[0].Rows)
                                {
                                    
                                    string create2 = @"INSERT INTO User_NotificationType (idUser, idNotificationType, status)
                                                 VALUES (@idUser, @idNotificatioType, @status)";

                                    using (SqlConnection conn8 = new SqlConnection(connectionString: conex))
                                    {
                                        SqlCommand command8 = new SqlCommand(create2, conn8);
                                        command8.Parameters.AddWithValue("@idUser", IdUser);
                                        command8.Parameters.AddWithValue("@idNotificatioType", _dr["idNotificationType"].ToString());
                                        command8.Parameters.AddWithValue("@status", 1);
                                        conn8.Open();
                                        command8.ExecuteScalar();
                                    }
                                }
                            }
                            status = "Success";
                    }
                    else 
                    {
                        status = "Error";
                    }
             

            }

            return status;
        
        }

        public static List<Data_PushBBVA.Notification> List(string rut, string type){


            string sql = @"SELECT N.idNotification, N.status, NT.idNotificationType ,  N.creation, N.delivery, U.firstName, U.lastName, U.rut, NT.title, NT.text, D.token, P.description 
                            FROM dbo.Notification N, dbo.Users U, dbo.NotificationType NT, dbo.Device D, dbo.Plataform P
                            WHERE P.idPlataform = D.idPlataform  AND D.idDevice = N.idDevice AND NT.idNotificationType = N.idNotificationType AND N.status = @type AND N.idUser = U.idUser AND U.rut = @rut"; 
            

             using (SqlConnection conn = new SqlConnection(connectionString: conex))
             {
                 
               

                    List<Data_PushBBVA.Notification> notification = new List<Data_PushBBVA.Notification>();

                     SqlCommand command = new SqlCommand(sql, conn);

                     command.Parameters.AddWithValue("@rut", rut);
                     command.Parameters.AddWithValue("@type", type);

                     conn.Open();

                     SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
                     DataSet dtDatos = new DataSet();
                     daAdaptador.Fill(dtDatos);

                     foreach (DataRow _dr in dtDatos.Tables[0].Rows)
                     {
                         Data_PushBBVA.Notification Notifi = new Data_PushBBVA.Notification();
                         Notifi.idNotification = int.Parse(_dr[0].ToString());
                         Notifi.name = _dr[5].ToString() + " " + _dr[6].ToString();
                         Notifi.create = DateTime.Parse(_dr[3].ToString());
                         Notifi.delivery = DateTime.Parse(_dr[4].ToString());
                         Notifi.tipo = _dr[2].ToString();
                         Notifi.rut = _dr[7].ToString();
                         Notifi.text = _dr[9].ToString();
                         Notifi.title = _dr[8].ToString();
                         Notifi.token = _dr[10].ToString();
                         Notifi.plataform = _dr[11].ToString();


                         notification.Add(Notifi);
                     }


                     return notification;
                 
             }

        
        
        }

        public static List<Data_PushBBVA.Notification> ListNotification(string type){


            string sql = @"SELECT N.idNotification, N.status, NT.idNotificationType ,  N.creation, N.delivery, U.firstName, U.lastName, U.rut, NT.title, NT.text, D.token, P.description 
                            FROM dbo.Notification N, dbo.Users U, dbo.NotificationType NT, dbo.Device D, dbo.Plataform P
                            WHERE P.idPlataform = D.idPlataform  AND D.idDevice = N.idDevice AND NT.idNotificationType = N.idNotificationType AND N.idUser = U.idUser AND N.status = @type"; 
           

             using (SqlConnection conn = new SqlConnection(connectionString: conex))
             {
                 
               

                    List<Data_PushBBVA.Notification> notification = new List<Data_PushBBVA.Notification>();

                     SqlCommand command = new SqlCommand(sql, conn);

                   
                     command.Parameters.AddWithValue("@type", type);

                     conn.Open();

                     SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
                     DataSet dtDatos = new DataSet();
                     daAdaptador.Fill(dtDatos);

                     foreach (DataRow _dr in dtDatos.Tables[0].Rows)
                     {
                         Data_PushBBVA.Notification Notifi = new Data_PushBBVA.Notification();
                         Notifi.idNotification = int.Parse(_dr[0].ToString());
                         Notifi.name = _dr[5].ToString() + " " + _dr[6].ToString();
                         Notifi.create = DateTime.Parse(_dr[3].ToString());
                         Notifi.delivery = DateTime.Parse(_dr[4].ToString());
                         Notifi.tipo = _dr[2].ToString();
                         Notifi.rut = _dr[7].ToString();
                         Notifi.text = _dr[9].ToString();
                         Notifi.title = _dr[8].ToString();
                         Notifi.token = _dr[10].ToString();
                         Notifi.plataform = _dr[11].ToString();


                         notification.Add(Notifi);
                     }


                     return notification;
                 
             }

        
        
        }

        public static string UpdateDevice(string idDevice, string token) {

            string createUser = @"UPDATE dbo.Device SET token = @token WHERE idDevice = @idDevice";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString: conex))
                {
                    SqlCommand command = new SqlCommand(createUser, conn);
                    command.Parameters.AddWithValue("@idDevice", idDevice);
                    command.Parameters.AddWithValue("@token", token);
                    conn.Open();
                    command.ExecuteScalar();

                    return "Success";
                }
            }
            catch (Exception ex)
            {
                return "Error";
            }
            
        }

        public static List<Data_PushBBVA.NotificationUser> ListSetting(string rut) {


            string sql = @"SELECT NTU.status, NT.title, NT.idNotificationType
FROM dbo.Users U, dbo.NotificationType NT, dbo.User_NotificationType NTU
WHERE NT.idNotificationType = NTU.idNotificationType AND NTU.idUser = U.idUser AND U.rut = @rut";


            using (SqlConnection conn = new SqlConnection(connectionString: conex))
            {



                List<Data_PushBBVA.NotificationUser> Setting = new List<Data_PushBBVA.NotificationUser>();

                SqlCommand command = new SqlCommand(sql, conn);


                command.Parameters.AddWithValue("@rut", rut);

                conn.Open();

                SqlDataAdapter daAdaptador = new SqlDataAdapter(command);
                DataSet dtDatos = new DataSet();
                daAdaptador.Fill(dtDatos);

                foreach (DataRow _dr in dtDatos.Tables[0].Rows)
                {
                    Data_PushBBVA.NotificationUser setting = new Data_PushBBVA.NotificationUser();
                    setting.status = _dr[0].ToString();
                    setting.title = _dr[1].ToString();
                    setting.idNotificationType = int.Parse(_dr[2].ToString());
                    Setting.Add(setting);
                }


                return Setting;

            }

        
        
        }
      
    }
}
