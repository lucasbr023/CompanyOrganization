using System.Collections.Generic;

namespace CompanyOrganization.Contract
{
    public interface ILoadService
    {
        /// <summary>
        /// Read a csv file.
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns>Lines of the csv file</returns>
        IList<string> GetLinesCsvFile(string filePath);

        /// <summary>
        /// Get file path
        /// </summary>
        /// <param name="fileName">file name</param>
        /// <returns>complete file path</returns>
        string GetFilePath(string fileName);
    }
}
