
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

interface IDataSource
{
    public string? FileName { get; set; }
    void WriteData(string data);
    string ReadData();
}


class FileDataSource : IDataSource
{
    public string? FileName { get; set; }
    public FileDataSource(string path)
    {
        FileName = path;
    }

    public void WriteData(string data)
    {
        using FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate);
        using StreamWriter sw = new StreamWriter(fs);
        sw.Write(data);
    }

    public string ReadData()
    {   
        using StreamReader sr = new StreamReader(FileName);
        string data = sr.ReadToEnd();
        return data;
    }
}



class Application
{
    private readonly IDataSource _datasource;
    public Application(IDataSource datasource)
    {
        _datasource = datasource;
    }

    public void WriteData(string data)
    {
        _datasource.WriteData(data);
    }

    public string ReadData(string path)
    {
        return _datasource.ReadData();
    }

}


abstract class DataSourceDecorator : IDataSource
{
    public string? FileName { get; set; }
    protected IDataSource _wrappe { get; set; }

    public DataSourceDecorator(IDataSource datasource,string file)
    {
        _wrappe = datasource;
        FileName = file;
    }

    public virtual void WriteData(string message)
    {
        //TODO write data
    }

    public virtual string ReadData()
    {
        //TODO read data
        return "";
    }
}


class EncryptionDecorator : DataSourceDecorator
{
    char xorKey = 'P';
    public EncryptionDecorator(string FileName,IDataSource? datasource= null)
        : base(datasource, FileName) { }

    public override string ReadData()
    {
        using StreamReader sr = new StreamReader(FileName);
        string data = sr.ReadToEnd();
        var decrypted = new StringBuilder();
        string outputString = "";
        for (int i = 0; i < data.Length; i++)
        {
            outputString = outputString +
            char.ToString((char)(data[i] ^ xorKey));
        }
        return outputString.ToString();

    }
    public override void WriteData(string data)
    {
       
        int len = data.Length;
        string outputString = "";

        // perform XOR operation of key
        // with every character in string
        for (int i = 0; i < len; i++)
        {
            outputString = outputString +
            char.ToString((char)(data[i] ^ xorKey));
        }

        using FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate);
        using StreamWriter sw = new StreamWriter(fs);
        sw.Write(outputString);

        //TODO write data
    }
}



class CompressionDecorator : DataSourceDecorator
{
    public CompressionDecorator(IDataSource datasource,string FileName)
        : base(datasource,FileName) { }

    public override string ReadData()
    {
        //TODO read data
        return "";
    }
    public override void WriteData(string message)
    {
        //TODO write data
    }
}

class Program
{
    static void Main()
    {
        string FileName = "myFile.txt";
        string path = @$"C:\Users\{Environment.UserName}\Desktop\{FileName}";
        IDataSource dataSource = new FileDataSource(path);
        dataSource = new EncryptionDecorator(path);
        dataSource.WriteData("My name is Murad");
        Console.WriteLine(dataSource.ReadData());


        //Application client = new(dataSource);
        //Console.WriteLine(client.ReadData(FileName));

        //dataSource.WriteData("My name is Leyla");
        //Console.WriteLine(dataSource.ReadData());
        //INotifier notifier = new Email();

        //notifier = new TelegramDecorator(notifier);
        //notifier = new SlackDecorator(notifier);
        //notifier = new FacebookDecorator(notifier);



        //Application client = new(notifier);
        //client.SendMessage("Discount 50%");





    }
}