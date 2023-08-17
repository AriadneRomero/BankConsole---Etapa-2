using System.Text.RegularExpressions;
using BankConsole;

if(args.Length == 0)
    ShowMenu();
else
    ShowMenu();

void ShowMenu(){
    Console.Clear();
    Console.WriteLine("Seleccione una opción:");
    Console.WriteLine("1 - Crear un Usuario Nuevo.");
    Console.WriteLine("2 - Eliminar un Usuario Existente.");
    Console.WriteLine("3 - Salir");

    int option = 0;
    do {
        string input = Console.ReadLine();
        if (!int.TryParse(input, out option)) 
            Console.WriteLine("Debes de ingresar un número (1 o 2 o 3).");
        else if (option > 3) 
            Console.WriteLine("Debes de ingresar un número valido (1 o 2 o 3).");

    } while (option == 0 || option >3);

    switch (option) {
        case 1:
        Console.WriteLine("Opcion 1");
            CreateUser();
            break;

        case 2:
        Console.WriteLine("Opcion 2");
            DeleteUser();
            break;

        case 3: 
            Environment.Exit(0);
            break;
    }
}

void CreateUser(){

    Console.Clear();
    Console.WriteLine("Ingresa la información del usuario:");

//---------ID-----------------
    int ID;
    int v=0;
    bool ValidID;

    do{
        ValidID=true;
        bool ID_Obtenido = true;
        Console.Write("ID: ");
        ID = validacion(v);

        ID_Obtenido = Storage.GetIDUser(ID);

        if(ID<0 || ID_Obtenido){
            Console.WriteLine("El ID es negativo, o ya se encuentra registrado");
            ValidID = false;
        }

    }while(!ValidID);

//---------Nombre----------------
    Console.Write("Nombre: ");
    string name = Console.ReadLine();

//-----------Email----------------
    
    bool ValidEmail;
    string email;
    do{
        ValidEmail=true;
        Console.Write("Email: ");
        email = Console.ReadLine();

        if(!IsValidEmail(email)){
            Console.WriteLine("El formato del correo electrónico es invalido.");
            ValidEmail=false;
        }

    }while(!ValidEmail);

    static bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

//-----------Balance----------------
    decimal balance;
    bool ValidB;
    do{
        Console.Write("Saldo: ");

        if(!decimal.TryParse(Console.ReadLine(), out balance)){
            Console.WriteLine("El balance es invalido");
            ValidB = false;
        }

        if(balance<0){
            Console.WriteLine("El balance es negativo");
            ValidB = false;
        }
        else
            ValidB = true;
        
    }while(!ValidB);
    
//-----------C o E---------------
    bool ValidC;
    char userType;
    do{
        ValidC = true;
        Console.Write("Escriba 'c' si el usuario es Cliente, 'e' si es Empleado: ");
        userType = char.Parse(Console.ReadLine());

        if(userType!= 'c' && userType!= 'e'){
            Console.WriteLine("Caracter inválida");
            ValidC = false;
        }

    }while(!ValidC);

//----------------------------------------------------

    User newUser;

    if (userType.Equals('c'))
    {
        Console.Write("Regimen Fiscal: ");
        char taxRegime = char.Parse(Console.ReadLine());

        newUser = new Client(ID,name,email,balance,taxRegime);
    } else
    {
        Console.Write("Departamento: ");
        string departament = Console.ReadLine();

        newUser = new Employee(ID, name, email, balance, departament);
    }

    Storage.AddUser(newUser);

    Console.WriteLine("Usuario creado");
    Thread.Sleep(2000);
    ShowMenu();
}

void DeleteUser(){
    bool ValidDelete;
    int v=0;
    Console.Clear();
    int ID;

    do{
        ValidDelete=true;
        bool ID_Obtenido = true;
        Console.Write("Ingrese el ID del usuario a eliminar: ");
        ID = validacion(v);

        ID_Obtenido = Storage.GetIDUser(ID);

        if(ID<0 || !ID_Obtenido){
            Console.WriteLine("El ID es negativo, o no se encuentra registrado");
            ValidDelete = false;
        }

    }while(!ValidDelete);

    string result = Storage.DeleteUser(ID);

    if (result.Equals("Success"))
    {
        Console.Write("Usuario eliminado");
        Thread.Sleep(2000);
        ShowMenu();
    }
}

//Validacion enteros
int validacion(int v){
    bool valor = false;

    while(!valor){
        if(int.TryParse(Console.ReadLine(), out v)){
             valor = true;
             return v;
        }
        else{
            Console.WriteLine("El valor ingresado es inválido. Vuelve a ingresalo.");
            Console.WriteLine("Ingrese de nuevo: ");
        }
    }
    return 0;
}







