using DAL;
namespace Service
{
    public class DAOService
    {
        private DAO dao;

        public DAOService()
        {
            this.dao = new DAO();
        }

        public void getEmployees()
        {
            dao.getEmployees();
        }

        public void getTickets()
        {
            dao.getTickets();
        }
    }
}