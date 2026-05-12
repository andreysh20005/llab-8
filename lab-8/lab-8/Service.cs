
internal class Service
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string CompanyName { get; set; }
    public int Discount { get; set; }
    public bool IsActive { get; set; }

    public Service() { }
    public Service(string name, double price, string companyName, int discount, bool isActive)
    {
        this.Name = name;
        Price = price;
        CompanyName = companyName;
        Discount = discount;
        IsActive = isActive;
    }
    public Service(Service other)
    {
        Name = other.Name;
        Price = other.Price;
        CompanyName= other.CompanyName;
        Discount= other.Discount;
        IsActive= other.IsActive;
    }

    public static Service ManualCreateService()
    {
        string name = Interface.InputString("Введите название услуги");
        double price = Interface.InputNum("введите полную цену услуги");
        string companyName = Interface.InputString("введите название компании, оказывающей услуги");
        int discount = Interface.InputNum("введите существующую скидку (0 если полная цена, 100 если бесплатно)");
        bool isActive = Interface.InputBool("Если услуга доступна сейчас, введите 'Y', иначе введите 'N'");
        Service newData = new Service(name, price, companyName, discount, isActive);
        return newData;
    }

    public override string ToString()
    {
        string result = $"{Name} от компании {CompanyName}. Цена: {Price}. ";
        if (Discount > 0)
        {
            result += $"скидка {Discount}%. ";
        }
        else
        {
            result += "скидка отсутствует. ";
        }
        if (IsActive)
        {
            result += "Услуга доступна на данный момент";
        }
        else
        {
            result += "услуга недоступна в данный момент";
        }
        return result;
    }
}

