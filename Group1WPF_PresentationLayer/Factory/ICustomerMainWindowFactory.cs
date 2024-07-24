using Assignment2.View.CustomerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Factory {
    public interface ICustomerMainWindowFactory {
        CustomerMainWindow Create(int customerId);
    }

}
