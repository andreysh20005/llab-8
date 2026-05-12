using static System.Runtime.InteropServices.JavaScript.JSType;

internal class DataBase
{
    private const string DBFileName = "Service_DB.dat";
    public static void SaveAll(List<Service> services)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(DBFileName, FileMode.Create)))
        {
            foreach (Service data in services)
            {
                writer.Write(data.Name);
                writer.Write(data.Price);
                writer.Write(data.CompanyName);
                writer.Write(data.Discount);
                writer.Write(data.IsActive);
            }
        }
    }

    public static List<Service> ReadAll()
    {
        var list = new List<Service>();
        if (!File.Exists(DBFileName))
        {
            return list;
        }

        using (BinaryReader reader = new BinaryReader(File.OpenRead(DBFileName)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                list.Add(new Service
                {
                    Name = reader.ReadString(),
                    Price = reader.ReadDouble(),
                    CompanyName = reader.ReadString(),
                    Discount = reader.ReadInt32(),
                    IsActive = reader.ReadBoolean()

                });
            }
        }
        return list;
    }
    
    public static List<Service> GetAllActiveServices()
    {
        return (
            from data in ReadAll()
            where data.IsActive == true
            orderby data.Name
            select data).ToList();
    }
    public static List<Service> GetWithCurrentDiscount(int discount)
    {
        return ( from data in ReadAll()
                 where data.Discount == discount
                 orderby data.Name
                 select data).ToList();
    }
    public static List<Service> GetRealPrice()
    {
        return (from data in ReadAll()
                select CalculateRealPrice(data)
                ).ToList();

    }
    public static Service FindByName(string name)
    {
        return ( from data in ReadAll()
                 where data.Name == name
                 select data).FirstOrDefault();
    }
    public static Service GetServiceRealPrice(string name)
    {
        return (from data in ReadAll()
                where data.Name == name
                select CalculateRealPrice(data)
                ).FirstOrDefault();
    }
    private static Service CalculateRealPrice(Service service)
    {
        if (service==null)
        {
            return null;
        }
        double realPrice = service.Price * ((100.0 - service.Discount) / 100.0);
        return new Service(
                    service.Name,
                    realPrice,
                    service.CompanyName,
                    0,
                    service.IsActive
                );
    }


    public static void DeleateByName(string name)
    {
        SaveAll(
            (from data in ReadAll()
             where data.Name != name
             select data
             ).ToList());
    }
    public static void AddService(Service service)
    {
        var data = ReadAll();
        data.Add(service);
        SaveAll(data);
    }
    public static void AddService()
    {
        Service service = Service.ManualCreateService();
        var data = ReadAll();
        data.Add(service);
        SaveAll(data);
    }

}