using CompanyEmployeesBalancing.Domain.BusinessObjects;

namespace CompanyEmployeesBalancing.Services.Contract
{
    public interface IBalanceService
    {
        /// <summary>
        /// Balance employees between teams
        /// </summary>
        /// <returns>Company with balanced teams</returns>
        void Balance();

        /// <summary>
        /// string containing information of balance
        /// </summary>
        /// <param name="company">company</param>
        /// <returns></returns>
        string BalanceToString(Company company);
    }
}
