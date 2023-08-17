//Esta clase es almacenar en un archivo json los objetos tipo usuarios que 
//vamos creando
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace BankConsole;

public static class Storage
{
    static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

    public static void AddUser(User user)
    {
        string json = "", usersInFile = "";

        if(File.Exists(filePath))
            usersInFile = File.ReadAllText(filePath);

        var listUsers = JsonConvert.DeserializeObject<List<object>>(usersInFile);
        //object: Cualquier tipo de objeto

        if(listUsers == null)
            listUsers = new List<object>();

        listUsers.Add(user);

        JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        //Definir propiedades al proceso de desearilzacion, que sea mas legibles

        json = JsonConvert.SerializeObject(listUsers, settings);
        //Serializar: convetir un objeto (C#) a Json
        //Deserializar: convetir Json a objeto (#C)

        File.WriteAllText(filePath, json);
    }

    public static List<User> GetNewUsers()
    {
        //Ver primero si hay informacion
        string usersInFile = "";
        var listUsers = new List<User>();

        if(File.Exists(filePath))
            usersInFile = File.ReadAllText(filePath);

        var listObjects = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if(listObjects == null)
            return listUsers; //Si no hay informacion no devolvemos lista de usuarios

        //Si hay info iteramos cada objeto
        foreach (object obj in listObjects)
        {
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();

            listUsers.Add(newUser);
        }

        var newUsersList = listUsers.Where(user => user.GetRegisterDate().Date.Equals(DateTime.Today)).ToList();
        //=>: parametro => resultado de la expresion

        return newUsersList;
    }

    public static string DeleteUser(int ID) {

        string usersInFile = "";
        var listUsers = new List<User>();

        if (File.Exists (filePath))
            usersInFile = File.ReadAllText (filePath);

        var listObjects = JsonConvert.DeserializeObject<List<object>> (usersInFile);

        if (listObjects == null)
            return "There are no users in the file.";

        foreach(object obj in listObjects)
        {
            User newUser;
            JObject user = (JObject)obj;
            if (user.ContainsKey("TaxRegime")) 
                newUser = user.ToObject<Client>();
            else
                newUser = user.ToObject<Employee>();

            listUsers.Add(newUser);
        }

        var userToDelete = listUsers.Where(user => user.GetID() == ID).Single();

        listUsers.Remove(userToDelete);

        JsonSerializerSettings settings = new JsonSerializerSettings { Formatting = Formatting. Indented };
        
        string json = JsonConvert.SerializeObject(listUsers, settings);
        
        File.WriteAllText(filePath, json);
        return "Sucess";
    }

     public static bool GetIDUser(int ID)
    {
        string usersInFile = "";
        if (File.Exists(filePath))
            usersInFile = File.ReadAllText(filePath);

        var listUsers = JsonConvert.DeserializeObject<List<User>>(usersInFile);

        if (listUsers == null)
            return false;

        foreach (var userIDJ in listUsers)
        {
            if (userIDJ.GetID() == ID)
            {
                return true; //Encontro coincidencia
            }
        }
        return false; //No encontro coincidencia
    }

}