interface IMobileShop
{
    string ModelNo();
    double Price();
}


class IPhone : IMobileShop
{
    public string ModelNo()=>"IPhone 6";
    

    public double Price()=>2000;
    
}

class Samsung : IMobileShop
{
    public string ModelNo() => "Samsung A72";

    public double Price()=>1000;
}

class BlackBerry : IMobileShop
{
    public string ModelNo()
    => "BlackBerry";

    public double Price() => 800;
}


public class ShopKeeper
{
    private IMobileShop iphone;
    private IMobileShop samsung;
    private IMobileShop blackberry;

    public ShopKeeper()
    {
        iphone = new IPhone();
        samsung = new Samsung();
        blackberry = new BlackBerry();
    }

    public void iphoneSale()
    {
        Console.WriteLine($"{iphone.ModelNo()} - {iphone.Price()}");
    }
    public void samsungSale()
    {
        Console.WriteLine($"{samsung.ModelNo()} - {samsung.Price()}");
    }
    public void blackberrySale()
    {
        Console.WriteLine($"{blackberry.ModelNo()} - {blackberry.Price()}");
    }
}

class Program
{
    static void Main()
    {
        ShopKeeper facade = new ShopKeeper();
        facade.iphoneSale();
        facade.samsungSale();
        facade.blackberrySale();

    }
}