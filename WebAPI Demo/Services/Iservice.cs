using System.Data;
using WebAPI_Demo.Models;

namespace WebAPI_Demo.Services
{
    public interface Iservice
    {
        void SendEmail();
        void InsertRecords(car car);

        DataTable SyncData();

        List<car> GetAllRecords();
    }
}
