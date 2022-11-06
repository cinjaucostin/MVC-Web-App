using MVCpractice.Models;

namespace MVCpractice.ViewModels
{
    public class CustomerIndexVM : ItemVM
    {
        public List<Customer> Customers { get; set; }
    }
}
