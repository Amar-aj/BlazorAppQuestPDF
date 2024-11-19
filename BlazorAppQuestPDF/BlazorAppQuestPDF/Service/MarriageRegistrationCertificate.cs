namespace BlazorAppQuestPDF.Service;
public class MarriageRegistrationCertificate
{
    public string RegdNo { get; set; }
    public DateTime DateOfApplication { get; set; }
    public Person Husband { get; set; }
    public Person Wife { get; set; }
    public DateTime DateOfMarriage { get; set; }
    public string PlaceOfMarriage { get; set; }
    public DateTime HusbandDateOfBirth { get; set; }
    public DateTime WifeDateOfBirth { get; set; }
    public Guardian HusbandGuardian { get; set; }
    public Guardian WifeGuardian { get; set; }
    public Witness[] Witnesses { get; set; }
    public string Remark { get; set; }
    public string MemoNo { get; set; }
    public string Registrar { get; set; }
    public string Block { get; set; }
    public string District { get; set; }

    public MarriageRegistrationCertificate(string regdNo, DateTime dateOfApplication,
        Person husband, Person wife, DateTime dateOfMarriage, string placeOfMarriage,
        DateTime husbandDOB, DateTime wifeDOB,
        Guardian husbandGuardian, Guardian wifeGuardian,
        Witness[] witnesses, string remark, string memoNo, string registrar, string block, string district)
    {
        RegdNo = regdNo;
        DateOfApplication = dateOfApplication;
        Husband = husband;
        Wife = wife;
        DateOfMarriage = dateOfMarriage;
        PlaceOfMarriage = placeOfMarriage;
        HusbandDateOfBirth = husbandDOB;
        WifeDateOfBirth = wifeDOB;
        HusbandGuardian = husbandGuardian;
        WifeGuardian = wifeGuardian;
        Witnesses = witnesses;
        Remark = remark;
        MemoNo = memoNo;
        Registrar = registrar;
        Block = block;
        District = district;
    }
}

public class Person
{
    public string Name { get; set; }
    public string Address { get; set; }

    public Person(string name, string address)
    {
        Name = name;
        Address = address;
    }
}

public class Guardian
{
    public string Name { get; set; }
    public string Address { get; set; }

    public Guardian(string name, string address)
    {
        Name = name;
        Address = address;
    }
}

public class Witness
{
    public string Name { get; set; }
    public string Address { get; set; }

    public Witness(string name, string address)
    {
        Name = name;
        Address = address;
    }
}