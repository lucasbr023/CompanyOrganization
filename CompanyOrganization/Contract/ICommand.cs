namespace CompanyOrganization.Contract
{
    public interface ICommand
    {
        string Execute(string parameters = null);
    }
}
