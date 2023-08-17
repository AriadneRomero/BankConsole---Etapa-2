namespace BankConsole;

public abstract class Person
{
    public abstract string GetName();
    //Al ser abstrato se declara pero no se implementa

    public string GetCountry() //metodo no
    {
        return "México";
    }
}

public interface IPerson //Todos los elementos de la interfaz es Abstracta
{
    string GetName(); //Ambos son abstratos, estan definidos pero no implementados
    string GetCountry();
}