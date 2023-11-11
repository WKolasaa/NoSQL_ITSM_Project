using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class FileIOService
    {
        // Individual Functionality 2 - Archiving Database - Ignas
        private FileIODAO fileIODAO;
        public FileIOService() {
            this.fileIODAO = new FileIODAO();
        }

        public void createBackup()
        {
            fileIODAO.createBackup();
        }
    }
}
