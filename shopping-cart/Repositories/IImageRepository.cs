
using SA51_CA_Project_Team10.Models.Domain;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}